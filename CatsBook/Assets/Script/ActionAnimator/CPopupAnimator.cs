using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPopupAnimator : CActionAnimator
{
    public enum PopupState
    {
        Open,
        Opening,
        Close,
        Closing
    }

    public enum AnimationType
    {
        Scale,
        Fade
    }

    public PopupState popupState { get; set; } = PopupState.Close;

    [SerializeField] private AnimationType animType = AnimationType.Scale;
    [SerializeField] private float openDuration = 0.45f;
    [SerializeField] private Ease openEase = Ease.OutBack;
    [SerializeField] private float closeDuration = 0.1f;
    [SerializeField] private Ease closeEase = Ease.OutBack;

    [SerializeField] private float openDelay = 0f;

    public float OpenDelay
    {
        get { return openDelay; }
        set { openDelay = value; }
    }

    public float OpenDuration
    {
        get { return openDuration; }
        set { openDuration = value; }
    }
    public float CloseDuration
    {
        get { return openDuration; }
        set { openDuration = value; }
    }
    public Ease OpenEase
    {
        get { return openEase; }
        set { openEase = value; }
    }
    public Ease CloseEase
    {
        get { return closeEase; }
        set { closeEase = value; }
    }

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Vector3 originPosition;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();

        originPosition = rectTransform.position;
    }

    private Sequence FadeAnimationEnter()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 1.0f;
        return DOTween.Sequence()
            .Append(canvasGroup.DOFade(1.0f, openDuration).SetEase(openEase).SetUpdate(true).SetDelay(openDelay));
    }

    private Sequence FadeAnimationEscape()
    {
        return DOTween.Sequence()
            .Append(canvasGroup.DOFade(0.0f, closeDuration).SetEase(closeEase).SetUpdate(true));
    }

    private Sequence ScaleAnimationEnter()
    {
        gameObject.SetActive(true);
        originPosition = rectTransform.position;
        if (openDelay > 0)
        {
            transform.localScale = Vector3.zero;
        }
        else
        {
            transform.localScale = Vector3.one * 0.8f;
        }
        
        canvasGroup.alpha = 1.0f;
        return DOTween.Sequence()
            .Append(transform.DOScale(1.0f, openDuration).SetEase(openEase).SetUpdate(true).SetDelay(openDelay));
    }

    private Sequence ScaleAnimationEscape()
    {
        return DOTween.Sequence()
            .Append(canvasGroup.DOFade(0.0f, closeDuration).SetEase(closeEase).SetUpdate(true));
    }

    protected override IEnumerator onAnimationEnter()
    {
        Sequence sequence = null;
        switch (animType)
        {
            case AnimationType.Fade:
                sequence = FadeAnimationEnter();
                break;
            case AnimationType.Scale:
                sequence = ScaleAnimationEnter();
                break;
            //Tip: 팝업의 애니메이션을 여러개 구성할 수 있음
        }
        yield return new WaitForSecondsRealtime(sequence.Duration());
    }

    protected override IEnumerator onAnimationEscape()
    {
        Sequence sequence = null;
        switch (animType)
        {
            case AnimationType.Fade:
                sequence = FadeAnimationEscape();
                break;
            case AnimationType.Scale:
                sequence = ScaleAnimationEscape();
                break;
            //Tip: 팝업의 애니메이션을 여러개 구성할 수 있음
        }
        yield return new WaitForSecondsRealtime(sequence.Duration());
    }

    protected override void onAnimationEnterBegin()
    {
        base.onAnimationEnterBegin();

        StopAllCoroutines();
        gameObject.SetActive(true);
        popupState = PopupState.Opening;
    }

    protected override void onAnimationEnterFinish()
    {
        base.onAnimationEnterFinish();
        popupState = PopupState.Open;
    }

    protected override void onAnimationEscapeBegin()
    {
        base.onAnimationEscapeBegin();
        popupState = PopupState.Closing;
    }

    protected override void onAnimationEscapeFinish()
    {
        gameObject.SetActive(false);
        base.onAnimationEscapeFinish();
        popupState = PopupState.Close;
    }
}
