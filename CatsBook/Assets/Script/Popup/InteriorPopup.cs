using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorPopup : MonoBehaviour
{

    public RectTransform itemList;
    public int itemCnt = 20; //RectSize , ToDo..
    // Start is called before the first frame update
    void OnEnable()
    {
        int bottom = (20 / 3) * (-300) + 300;
        bottom = bottom < 0 ? bottom : 0;

        itemList.offsetMin = new Vector2(itemList.offsetMin.x, bottom);

    }

}
