using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPopupManager : Singleton<CPopupManager>
{
    public static readonly string POPUP_COMMON = "CommonPopup";

    private Dictionary<string, CBasicPopup> dicPopups = new Dictionary<string, CBasicPopup>();
    private string _nowPopup;
    public bool isOpen = false;
   
    public string nowPopup
    {
        get { return _nowPopup;}
        set
        {
            if(value == "")
            {
                isOpen = false;
                _nowPopup = value;
            }
            else
            {
                isOpen = true;
                _nowPopup = value;
            }
        }

    }

    public bool block = false;

    public CPopupManager()
    {
        nowPopup = "";
    }
   

    public bool isOpenPopup = false;
    public T openPopup<T>(T popup, string popupName) where T : CBasicPopup
    {
        
        if (isOpened(popupName))
            return getPopupByName<T>(popupName);

        if (!dicPopups.ContainsKey(popupName))
        {
            dicPopups.Add(popupName, popup);
            popup.open();
        }
        else
        {
            popup = getPopupByName<T>(popupName);
            popup.transform.SetAsLastSibling();
            popup.open();
        }

        //if (popup.hasAnimator())
        //{
            //popup.animator.actAnimationEscapeFinish += () => {
                //dicPopups.Remove(popupName);
            //};
        //}

        return popup;
    }

    public void openPopup(CBasicPopup popup, string popupName)
    {
        if (block) { return; }


        if (dicPopups.ContainsKey(popupName))
        {
            if(popup.PopupName == nowPopup)
            {
                isOpenPopup = false;

                nowPopup = "";
                dicPopups.Remove(popupName);
                popup.close();
            }
            else
            {
                allClosePopup();

                isOpenPopup = true;
                nowPopup = popupName;

                openPopup<CBasicPopup>(popup, popupName);
            }

        }
        else
        {

            allClosePopup();

            isOpenPopup = true;
            nowPopup = popupName;

            openPopup<CBasicPopup>(popup, popupName);

        }
    }

    public void closePopup(string popupName)
    {
        CBasicPopup popup = getPopupByName<CBasicPopup>(popupName);
        if (popup != null)
            closePopup(popup);

    }

    public void closePopup(CBasicPopup popup)
    {
        nowPopup = "";
        popup.close();
    }

    public void allClosePopup()
    {
        nowPopup = "";
        foreach (var popup in dicPopups.Values)
            closePopup(popup);
    }

    public bool isOpened(string popupName)
    {
        if (!dicPopups.ContainsKey(popupName))
            return false;
        CBasicPopup popup = getPopupByName<CBasicPopup>(popupName);
        return popup.isOpened();
    }

    public T getPopupByName<T>(string popupName) where T : CBasicPopup
    {
        if (!dicPopups.ContainsKey(popupName))
            return null;
        return (T)dicPopups[popupName];
    }

    public void blockUI()
    {
        block = true;
    }
    public void enableUI()
    {
        StartCoroutine(enable());
    }
    public IEnumerator enable()
    {
        yield return new WaitForSeconds(0.2f);
        block = false;

    }
}
