using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExteriorPopup : MonoBehaviour
{
    public RectTransform itemList;
    public int itemCnt = 0; //RectSize , ToDo..

    public GameObject item;
    public Transform Contents;

    void OnEnable()
    {
        setRectSize();// 수정필요
        updateItemList();

    }
    public void setRectSize()
    {
        itemCnt = GameManagerTemp.Instance.liExteriorItem.Count;
        int bottom = (itemCnt / 3) * (-300) + 300;
        bottom = bottom < 0 ? bottom : 0;
        itemList.offsetMin = new Vector2(itemList.offsetMin.x, bottom);
    }

    public void updateItemList()
    {
        for(int i=0; i< GameManagerTemp.Instance.liExteriorItem.Count; i++)
        {
            GameObject obj  = Instantiate(item, Contents);
            obj.SetActive(true);

            ExteriorItem exteriorItem = obj.GetComponent<ExteriorItem>();
            exteriorItem.setDataInfo(GameManagerTemp.Instance.liExteriorItem[i]);
        }

    }
}
