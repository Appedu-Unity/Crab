using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

//為答案標簽的腳本
//本物件上要有collider(有勾isTrigger)，跟關掉useGravity的Rigidbody
//本物件要有AnsTag的Tag
public class TagsTrigger : MonoBehaviour
{
    public Transform ParentThing;
    public Vector3 pos = new Vector2();

    // Start is called before the first frame update
    void Start()
    {

    }

    //void IDragHandler.OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    //{
    //    RectTransformUtility.ScreenPointToWorldPointInRectangle(GetComponent<RectTransform>(), eventData.position, Camera.main, out pos);
    //    transform.position = pos;
    //}
    //如果移到作答位置，就會依附到作答位置的階層下
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BlankAns")
        {
            if (other.gameObject.transform.childCount > 0)
            {
                //Blank space already taken.
            }
            else
            {
                this.transform.SetParent(other.gameObject.transform);
            }
        }
    }
    //如果移到其他地方會回到起始設定的位置
    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Untagged")
        {
            if (other.gameObject.name != "DragRange")
            {
                this.transform.parent = ParentThing;
                other.gameObject.tag = "BlankAns";
            }
        }
    }
    #region 設定一開始的Tag位置
    public void SetParent()
    {
        ParentThing = this.gameObject.transform.parent;
        GetComponent<RectTransform>().transform.localPosition = Vector3.zero;
    }
    #endregion
    public void BackParentPlace()
    {
        this.transform.parent.gameObject.tag = "BlankAns";
        this.transform.parent = ParentThing;
        GetComponent<RectTransform>().transform.localPosition = Vector3.zero;
    }
}
