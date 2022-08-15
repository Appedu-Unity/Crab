using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

//為放在雙手Controller的腳本
public class HandControl : MonoBehaviour
{
    public XRController Hand;
    //是否在拖曳標籤
    public bool isUsingTag,istrigger;
    public string TagName;
    //確定最後一個互動的物件
    public GameObject LastTagThing;
    //各種Tag名稱
    //AnsTag：可以拖曳的標籤
    //BlankAns：作答範圍，有該標簽的該物件要有Collider(有勾isTrigger)
    //Untagged：取消狀態_原本就有
    //TagsParent：AnsTag開始時放置的位置

    //(物件名稱固定)DragRange：可以移動的範圍，要有Collider

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //按下Trigger
        if (Hand.selectInteractionState.activatedThisFrame)
        {
            //確認有射線，並找到所有被射線打到的物體
            RaycastHit[] hits;
            hits = Physics.RaycastAll(Hand.gameObject.transform.position, Hand.gameObject.transform.forward, 100000.0F);
            for (int i = 0; i < hits.Length; i++)
            {
                //找到要拖曳的標簽
                if (hits[i].collider.tag == "AnsTag")
                {
                    isUsingTag = !isUsingTag;
                }
            }
            //沒有選到Tag時+不是在放開標籤時變抓取物件
            if (!isUsingTag)
            {
                if (!istrigger)
                {
                    Hand.model.GetComponent<Animator>().SetTrigger("Grab");
                }
            }
        }
        //在拖曳標簽時
        if (isUsingTag)
        {           
            int TagLayer = 20000;
            int PointLayer = 20000;
            RaycastHit[] hits;
            hits = Physics.RaycastAll(Hand.gameObject.transform.position, Hand.gameObject.transform.forward, 100000.0F);
            //確認碰到的物體是標簽
            for (int i = 0; i < hits.Length; i++)
            {
                //找到要拖曳的標簽
                if (hits[i].collider.tag == "AnsTag")
                {
                    TagLayer = i;
                    TagName = hits[i].collider.name;
                    //設定讓手有動作?

                }
                //拖曳範圍
                if (hits[i].collider.name == "DragRange")
                {
                    PointLayer = i;
                }
            }
            if (GameObject.Find(hits[TagLayer].collider.name).tag == "AnsTag")
            {
                //第一次抓標籤物件的時候到Point
                if (!istrigger)
                {
                    Hand.model.GetComponent<Animator>().SetTrigger("Point");
                }
                istrigger = true;        
                //找到標簽物件並移動
                LastTagThing = GameObject.Find(hits[TagLayer].collider.name);
                LastTagThing.GetComponent<RectTransform>().position = hits[PointLayer].point;
            }
            //標簽_鎖住其他沒有要移動的標籤
            StartCoroutine(Tags_Ignore_Raycast());
        }
        if(!isUsingTag)
        {
            istrigger = false;
            //沒有選取跟沒有抓Tag的時候回到Idle
            if (Hand.selectInteractionState.deactivatedThisFrame)
            {
                Hand.model.GetComponent<Animator>().SetTrigger("Idle");
            }
            //設定標籤位置在放置的地方
            LastTagThing.GetComponent<RectTransform>().transform.localPosition = Vector3.zero;
            LastTagThing = null;
            //不確定運作方式
            if (GameObject.Find(TagName).transform.parent.gameObject.tag == "BlankAns")
            {
                GameObject.Find(TagName).transform.parent.gameObject.tag = "Untagged";
            }
            StartCoroutine(Tags_Raycast_Reset());
        }
    }
    #region 標簽_鎖住其他沒有要移動的標籤
    IEnumerator Tags_Ignore_Raycast()
    {
        GameObject[] Tags_Obj;
        Tags_Obj = GameObject.FindGameObjectsWithTag("AnsTag");

        for (int i = 0; i < Tags_Obj.Length; i++)
        {
            Tags_Obj[i].layer = 2;
        }

        GameObject.Find(TagName).layer = 0;
        yield return null;
    }
    #endregion
    #region 標簽_解鎖標簽
    IEnumerator Tags_Raycast_Reset()//放完之後解除Ignore狀態
    {  
        GameObject[] Tags_Obj;
        Tags_Obj = GameObject.FindGameObjectsWithTag("AnsTag");

        for (int i = 0; i < Tags_Obj.Length; i++)
        {
            Tags_Obj[i].layer = 0;
        }
        yield return null;
    }
    #endregion
}
