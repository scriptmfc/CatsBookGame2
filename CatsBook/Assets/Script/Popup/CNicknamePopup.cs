using GameUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CNicknamePopup : CBasicPopup
{
    [SerializeField] private CToastPopup toastPopup;
    [SerializeField] private Text nickName;
    private Action callback;
    [SerializeField] Image iconImage;

    void Start()
    {
        string fileName = Enum.GetName(typeof(PetId), CGameManager.Instance.petId);
        string path = "Image_Login/" + fileName;
        iconImage.sprite = Resources.Load<Sprite>(path);
    }

        public void onUpdateData(Action _callback = null)
    {
        callback = _callback;
    }

    public void onClickOkBtn()
    {
        
        if (nickName.text.Length == 0)
        {
            toastPopup.open();  
            toastPopup.setMessage("닉네임을 입력해주세요.");
            return;
        }
        if (CGameUtils.checkNickName(nickName.text) == false)
        {
            //닉네임 규칙 틀림
            //_callback(-1);
            toastPopup.open();
            toastPopup.setMessage("특수문자를 사용하실 수 없습니다.");
            return;
        }
        if (nickName.text.Length > 10)
        {
            toastPopup.open();
            toastPopup.setMessage("닉네임을 10자 이상으로 사용하실 수 없습니다.");
            return;
        }

    //    Backend.BMember.GetUserInfo((bro) =>//SendQueue.Enqueue
    //    {
    //        if (bro.IsSuccess())
    //        {
    //            JsonData returnData = bro.GetReturnValuetoJSON();
    //            if (returnData != null)
    //            {
    //                if (returnData.Count > 0)
    //                {
    //                    for (int i = 0; i < returnData.Count; i++)
    //                    {
    //                        if (returnData[i]["nickname"] == null)
    //                        {
    //                            BackendReturnObject isbroname = Backend.BMember.CheckNicknameDuplication(nickName.text);
    //                            if (isbroname.IsSuccess())
    //                            {
    //                                Backend.BMember.CreateNickname(nickName.text);
    //                                CGameManager.Instance.nickName = nickName.text;
    //                                callback();
    //                            }
    //                            else
    //                            {
    //                                toastPopup.open();
    //                                if (isbroname.GetStatusCode() == "409")
    //                                {
    //                                    //중복                                        
    //                                    toastPopup.setMessage("중복된 닉네임이 있습니다.");
    //                                }
    //                                else if (isbroname.GetStatusCode() == "400")
    //                                {
    //                                    //닉네임 앞뒤 공백
    //                                    toastPopup.setMessage("닉네임 공백을 넣으실 수 없습니다.");
    //                                }
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    });
    }


}
