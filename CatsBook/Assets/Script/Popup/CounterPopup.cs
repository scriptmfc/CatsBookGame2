using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterPopup : CBasicPopup
{
    enum ItemList
    {
        Shelf = 1,
        Interior = 2,
        Exterior = 3,
        Garage = 4,
        Clerck = 5,
        Bank = 6,
        None = 0,
    };

    ItemList nowItemList = ItemList.Interior;
    public Transform contentsList;

    public void onClickItemButton(string itemName)
    {
        if(isObject(nowItemList.ToString()))
        {
            string now = nowItemList.ToString();
            contentsList.Find(now).gameObject.SetActive(false);
        }
        
        if(isObject(itemName))
        {
            contentsList.Find(itemName).gameObject.SetActive(true);
        }

        var ignoreCase = true;
        Enum.TryParse(itemName, ignoreCase, out nowItemList);

    }


    public bool isObject(string name)
    {
        if(contentsList.Find(name) == null)
        {
            return false;
        }

        return true;
    }


    public void onClickClose()
    {
        CPopupManager.Instance.closePopup(gameObject.GetComponent<CBasicPopup>());
    }

}
