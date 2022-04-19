using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CCharacterPopup : MonoBehaviour
{
    [SerializeField] PetId petId;
    [SerializeField] TextMeshProUGUI petName;

    public void Start()
    {
        string Name = Enum.GetName(typeof(PetId), petId);
        petName.text = Name;
    }

    public void onClickOkBtn()
    {
        CGameManager.Instance.petId = petId;
        CChracterSelectPopup parent = this.transform.parent.parent.gameObject.GetComponent<CChracterSelectPopup>();
        parent.callback();
    }

    public void inToIcon()
    {
        Vector3 scale = new Vector3(1.2f, 1.2f, 1.2f);
        this.transform.DOScale(scale, 0.2f);
    }

    public void outOfIcon()
    {
        Vector3 scale = new Vector3(1.0f, 1.0f, 1.0f);
        this.transform.DOScale(scale, 0.2f);

    }
}
