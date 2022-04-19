using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameUtils;

public class CToastPopup : CBasicPopup
{
    [SerializeField] private TextMeshProUGUI contentText;

    public void setMessage(string message)
    {
        contentText.text = message;
        StartCoroutine(delayClose());
    }

    IEnumerator delayClose()
    {
        yield return CGameUtils.WaitForSeconds(1f);
        close();
    }
}
