using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_QratingMessagePopUp : MonoBehaviour
{
    public Text bookname;
    public CBasicPopup popup;
    public void OnEnable()
    {
        CPopupManager.Instance.blockUI();
    }

    public void onClickClose()
    {
        CPopupManager.Instance.enableUI();
        popup.close();

    }

}



