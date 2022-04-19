using System.Collections.Generic;
using UnityEngine;

public class CSkillCoolTime
{
    public int index;
    public float cooltime;
}

public class CUIManager : MonoBehaviour
{
    private static CUIManager instance = null;

    public static CUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(CUIManager)) as CUIManager;
                if (instance == null)
                {
                    instance = new GameObject("Canvas", typeof(CUIManager)).GetComponent<CUIManager>();
                }
            }
            return instance;
        }
    }
    [Header("Camera")]
    [SerializeField] private Camera mainCamera;

    [Header("PopUp")]
    public Transform popupTrs;
    static Dictionary<string, CBasicPopup> dicPopupObject = new Dictionary<string, CBasicPopup>();

    public GameObject blockGameObject;

    private void Start()
    {
        foreach(Transform child in popupTrs.transform)
        {
            string name = child.gameObject.name;
            CBasicPopup popup = child.gameObject.GetComponent<CBasicPopup>();
            popup.PopupName = name;
            dicPopupObject.Add(name, popup);
        }

    }
    public void onInitUI()
    {
    }

    public void onClickBookStorageButton(string _name = "BookStorage")
    {
        string name = _name + "Popup";
        openPopup(name);
    }

    public void onClickBackPackButton(string _name = "BackPack")
    {
        string name = _name + "Popup";
        openPopup(name);
    }

    public void onClickCounterButton(string _name = "Counter")
    {
        string name = _name + "Popup";
        openPopup(name);
    }

    public void onClickVillageOrRoomButton()
    {
        CPopupManager.Instance.allClosePopup();
    }

    private void openPopup(string name)
    {
        //if(CGameManager.Instance.isPetDungeon || CGameManager.Instance.isGiantDungeon || CGameManager.Instance.isDamageDungeon )
        //{
        //    CToastPopup toast = CPopupManager.Instance.openPopup(popupOverlayTrs, "ToastPopup").GetComponent<CToastPopup>();
        //    toast.setMessage("던전 전투 중에는 사용하실수 없습니다.");
        //    return;
        //}
        //CSoundManager.Instance.onPlayEffectSound(EffectSound.click);



        if(dicPopupObject.ContainsKey(name))
        {
            CPopupManager.Instance.openPopup(dicPopupObject[name], name);
        }
        
    }
}
