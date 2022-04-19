using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using BackEnd;
using UnityEngine.Networking;
using LitJson;
using UnityEngine.UI;
using System;
//using Facebook.Unity;
using DG.Tweening;
using GameUtils;
using TMPro;
using DG;
//using UnityEngine.SignInWithApple;

public class CBackendManager : MonoBehaviour
{
    private static CBackendManager instance = null;
    public static CBackendManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(CBackendManager)) as CBackendManager;
                if (instance == null)
                {
                    instance = new GameObject("BackendManager", typeof(CBackendManager)).GetComponent<CBackendManager>();
                }
            }
            return instance;
        }
    }

    //[SerializeField] GameObject krTitle;
    //[SerializeField] GameObject usTitle;
    //[SerializeField] GameObject loadingAni;
    [SerializeField] private AudioSource bgmSource;


    [SerializeField] GameObject loginPanel;
    public Button bgButton;
    //[SerializeField] Button bgButton;
    //[SerializeField] CGoogleConnect googleConnect;
    //[SerializeField] CFacebookConnect facebookConnect;
    //[SerializeField] CAppleConnect appleConnect;

    [SerializeField] GameObject googleButton;
    //[SerializeField] GameObject loading;
    //[SerializeField] GameObject loadingFailPopup;

    //private BackendReturnObject backendReturnObject;
    //public SignInWithApple signInWithApple;
    [SerializeField] private TextMeshProUGUI loadingText;

    private int timerCount = 0;
    private float time = 50;
    public bool isLoading = false;
    public string userIndata = "";
    public bool isLogin = false;

    public string loginKey = "";
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
        if (loginKey == "")
        {
            loginKey = SystemInfo.deviceUniqueIdentifier;
        }

        if (PlayerPrefs.HasKey("BGM"))
        {
            bgmSource.volume = PlayerPrefs.GetFloat("BGM");
        }

        onPlayGameStart();
    }


    private void onPlayGameStart()
    {
        loadingText.DOFade(0.4f, 1).SetLoops(-1, LoopType.Yoyo);

#if !UNITY_EDITOR
                        CGameUtils.isServerStatus();
                        CGameUtils.isClinetVersion();
                        //googleConnect.initGoogle();
                        onConnectLogin(false);
#elif UNITY_EDITOR
        //onClickCustomLogin();
#endif



        // Backend 작업 필요
        //        Backend.InitializeAsync(true, callback =>
        //        {
        //            if (callback.IsSuccess())
        //            {
        //                if (!Backend.Utils.GetGoogleHash().Equals(""))
        //                {
        //                    Debug.Log("Lim.GetGoogleHash" + Backend.Utils.GetGoogleHash());
        //                }

        //#if !UNITY_EDITOR
        //                CGameUtils.isServerStatus();
        //                CGameUtils.isClinetVersion();
        //                googleConnect.initGoogle();
        //                onConnectLogin(false);
        //#elif UNITY_EDITOR
        //                onClickCustomLogin();
        //#endif
        //            }
        //        });
    }

    public void onClickOpenLoginPopup()
    {
        loginPanel.SetActive(true);
    }

    public void onClickCloseLoginPopup()
    {
        loginPanel.SetActive(false);
    }

    public void onClickCustomLogin()
    {
        if (isLogin)
        {
            return;
        }
        isLogin = true;

        DOTween.Sequence()
            .AppendInterval(0.1f)
            .AppendCallback(() =>
            {
                onConnectLogin(true);
                onCustomLogin();
            });
    }

    private void onCustomLogin()
    {
        //Backend.BMember.SignOut();
        //backendReturnObject = Backend.BMember.CustomSignUp(loginKey, loginKey, "SignUp");
        //CGameManager.Instance.userId = loginKey;
        //if (backendReturnObject.IsSuccess() == true)
        //{
        //    CGameManager.Instance.isFirstLogin = true;
        //    onConnectLogin();
        //}
        //else
        //{
        //    if (backendReturnObject.GetStatusCode() == "409")
        //    {
        //        onConnectLogin();
        //    }
        //}
    }

    private void onConnectLogin()
    {
        //backendReturnObject = Backend.BMember.CustomLogin(loginKey, loginKey, "Login");
        //Debug.Log(backendReturnObject.IsSuccess());
        //if (backendReturnObject.IsSuccess() == true)
        //{
        //    onConnectLogin(true);
        //}
        //else
        //{
        //    Invoke("login", 3f);
        //}
    }

    public bool isCustomUser()
    {
        bool iscustom = false;
        //backendReturnObject = Backend.BMember.CustomLogin(loginKey, loginKey, "Login");
        //if (backendReturnObject.IsSuccess() == true)
        //{
        //    iscustom = true;
        //}
        //else if (backendReturnObject.GetStatusCode() == "401")
        //{
        //    iscustom = false;
        //}
        return iscustom;
    }

    public void onConnectLogin(bool islogin = false)
    {
        loadingText.DOFade(1, 0.1f).OnComplete(() => {
            DOTween.Kill(loadingText);
        });
        if (islogin == true)
        {
            bgButton.enabled = false;
            onClickCloseLoginPopup();
            if (CGameManager.Instance.loginType != "Google")
            {
                CGameManager.Instance.loginType = "Guest";
            }
            StartCoroutine(onLoadingTimer());
            refreshToken();
        }
    }

    private void refreshToken()
    {
        /* 뒤끝서버 붙인 뒤 수정 필요 */
        StopAllCoroutines();
        CGameManager.Instance.setGameDataInit();
        /* end */

        //bool istoken = Backend.BMember.IsAccessTokenAlive().GetMessage() == "Success" ? true : false;
        //if (!istoken)
        //{
        //    backendReturnObject = Backend.BMember.RefreshTheBackendToken();
        //    if (backendReturnObject.IsSuccess() == true)
        //    {
        //        StopAllCoroutines();
        //        CGameManager.Instance.setGameDataInit();
        //    }
        //    else
        //    {
        //        if (backendReturnObject.GetErrorCode() == "408" || backendReturnObject.GetErrorCode() == "502")
        //        {
        //            if (!CGameManager.Instance.isClientUpdate)
        //            {
        //                PlayerPrefs.DeleteAll();
        //                Transform popuptrs = null;
        //                popuptrs = GameObject.Find("Canvas").transform;
        //                COneButtonPopup popup = CPopupManager.Instance.openPopup(popuptrs, "LoginOneButtonPopup").GetComponent<COneButtonPopup>();
        //                popup.onUpdateInfo("서버 통신이 원활하지 않으니 재접속 부탁드립니다.", () =>
        //                {
        //                    Application.Quit();
        //                });
        //            }
        //        }
        //        else
        //        {
        //            Invoke("refreshToken", 3f);
        //        }
        //    }
        //}
        //else
        //{
        //    StopAllCoroutines();
        //    CGameManager.Instance.setGameDataInit();
        //}
    }

    IEnumerator onLoadingTimer()
    {
        yield return CGameUtils.WaitForSeconds(60);
        PlayerPrefs.DeleteAll();
        Transform popuptrs = null;
        popuptrs = GameObject.Find("Canvas").transform;
        if (!CGameManager.Instance.isClientUpdate)
        {
            //COneButtonPopup popup = CPopupManager.Instance.openPopup(popuptrs, "LoginOneButtonPopup").GetComponent<COneButtonPopup>();
            //popup.onUpdateInfo("서버 통신이 원활하지 않으니 재접속 부탁드립니다.", () =>
            //{
            //    Application.Quit();
            //});
        }
    }
}
