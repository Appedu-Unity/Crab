using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//為控制答案腳本位置的腳本
public class RandomTagPlace : MonoBehaviour
{
    //設定標簽的位置，可設空物件
    public GameObject[] TagParents;
    //把標簽放進RandomItem，他就會自己排列
    public GameObject[] RandomItem;
    // Start is called before the first frame update
    void Start()
    {
        Reload();
    }
    //按照順序排列的程式
    public void SetOrderParent()
    {
        for (int i = 0; i < RandomItem.Length; i++)
        {
            RandomItem[i].transform.parent = TagParents[i].transform;
        }
        for (int i = 0; i < RandomItem.Length; i++)
        {
            RandomItem[i].GetComponent<TagsTrigger>().SetParent();
        }
    }
    //排列的程式
    public void Reload()
    {
        int[] mArray = new int[RandomItem.Length];

        //GameObject[] OriPos;
        //OriPos = GameObject.FindGameObjectsWithTag("TagsParent");

        List<int> RandomNum = new List<int>();

        for (int i = 0; i < RandomItem.Length; i++)
        {
            RandomNum.Add(i);
        }
        for (int i = 0; i < RandomItem.Length; i++)
        {
            int a = RandomNum[Random.Range(0, RandomItem.Length - i)];
            RandomNum.Remove(a);
            if (i < RandomItem.Length)
            {
                mArray[i] = a;
            }
        }
        for (int i = 0; i < RandomItem.Length; i++)
        {
            RandomItem[mArray[i]].transform.parent = TagParents[i].transform;
        }
        StartParent();
    }

    public void StartParent()
    {

        for (int i = 0; i < RandomItem.Length; i++)
        {
            RandomItem[i].GetComponent<TagsTrigger>().SetParent();
        }

        //yield return null;
    }
}
