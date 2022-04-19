using System;
using System.Collections;
using UnityEngine;

public abstract class CActionAnimator : MonoBehaviour
{
    enum AnimState
    {
        Playing,
        Stop
    }

    private AnimState animState = AnimState.Stop;

    public event Action ActAnimationEnterBegin = null;
    public event Action actAnimationEscapeBegin = null;
    public event Action actAnimationEnterFinish = null;
    public event Action actAnimationEscapeFinish = null;

    protected virtual void onAnimationEnterBegin()
    {
        animState = AnimState.Playing;
        ActAnimationEnterBegin?.Invoke();
    }

    protected virtual IEnumerator onAnimationEnter()
    {
        yield return null;
    }

    protected virtual IEnumerator onAnimationEnterSequence()
    {
        yield return StartCoroutine(onAnimationEnter());
        onAnimationEnterFinish();
    }

    protected virtual void onAnimationEnterFinish()
    {
        animState = AnimState.Stop;
        actAnimationEnterFinish?.Invoke();
    }

    protected virtual void onAnimationEscapeBegin()
    {
        animState = AnimState.Playing;
        actAnimationEscapeBegin?.Invoke();
    }

    protected virtual IEnumerator onAnimationEscape()
    {
        yield return null;
    }

    protected virtual IEnumerator onAnimationEscapeSequence()
    {
        yield return StartCoroutine(onAnimationEscape());
        onAnimationEscapeFinish();
    }

    protected virtual void onAnimationEscapeFinish()
    {
        animState = AnimState.Stop;
        actAnimationEscapeFinish?.Invoke();
    }

    public void RunAnimation()
    {
        onAnimationEnterBegin();
        StartCoroutine(onAnimationEnterSequence());
    }

    public void StopAnimation()
    {
        onAnimationEscapeBegin();
        StartCoroutine(onAnimationEscapeSequence());
    }
}