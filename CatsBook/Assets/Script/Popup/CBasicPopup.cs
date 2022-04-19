using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CBasicPopup : MonoBehaviour
{
    [SerializeField] protected GameObject[] viewObjs;

    public int opendViewIndex { get; set; } = 0;

    public string PopupName { get; set; } = "";
    public CPopupAnimator animator = null;

    protected virtual void init() { }

    public virtual void OnDisable() { }

    public void OnEnable()
    {
        init();
    }


    public void Awake()
    {
        if (animator == null)
            animator = gameObject.AddComponent<CPopupAnimator>();
    }


    protected virtual void onEnableView(int index)
    {
        for (int i = 0; i < viewObjs.Length; i++)
        {
            if (viewObjs[i] && viewObjs[i].activeSelf == true)
            {
                viewObjs[i].SetActive(false);
            }
        }
        if (viewObjs[index])
        {
            viewObjs[index].SetActive(true);
            opendViewIndex = index;
        }
    }


    public bool isOpened()
    {
        return (animator.popupState == CPopupAnimator.PopupState.Open || animator.popupState == CPopupAnimator.PopupState.Opening);
    }

    public bool isAnimating()
    {
        return (animator.popupState == CPopupAnimator.PopupState.Opening || animator.popupState == CPopupAnimator.PopupState.Closing);
    }

    public bool hasAnimator()
    {
        return animator != null;
    }

    public void setVisible(bool visible)
    {
        gameObject.SetActive(visible);
    }

    public void open()
    {
        if (hasAnimator())
            animator.RunAnimation();
        else
            setVisible(true);
    }

    public void close()
    {
        //if (CSoundManager.Instance != null)
        //{
        //    CSoundManager.Instance.onPlayEffectSound(EffectSound.click);
        //}
        
        CPopupManager.Instance.isOpenPopup = false;
        if (gameObject.activeSelf)
        {
            if (hasAnimator())
                animator.StopAnimation();
            else
                setVisible(false);
        }
    }

    public void toggle()
    {
        if (!isOpened())
        {
            open();
        }
        else
        {
            close();
        }
    }
}
