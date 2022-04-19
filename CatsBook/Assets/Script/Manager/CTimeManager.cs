using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using BackEnd;
using GameUtils;

public enum TIMEER_TYPE
{
    REMOVE,
    CONTINUITY
}

public enum TIMEER_TYPE_KEY
{
    REMOVE,
    CONTINUITY
}

public class TimeData
{
    public TIMEER_TYPE type;
    public string key;
    public float defaultTime;
    public float lastTime;
    public Action<float> callback;
    public Action endCallBack;
}

public class GoalTimeData
{
    public TIMEER_TYPE type;
    public string key;
    public DateTime defaultTime;
    public TimeSpan lastTime;
    public Action<TimeSpan> callback;
    public Action endCallBack;
}


public class CTimeManager : MonoBehaviour
{
    private static CTimeManager instance = null;
    public static CTimeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(CTimeManager)) as CTimeManager;
                if (instance == null)
                {
                    instance = new GameObject("CTimeManager", typeof(CTimeManager)).GetComponent<CTimeManager>();
                }
            }
            return instance;
        }
    }
    private List<TimeData> liTimeListener = new List<TimeData>();
    private List<GoalTimeData> liGoalTimeListener = new List<GoalTimeData>();
    private Coroutine timeCoroutine = null;
    private Coroutine goalTimeCoroutine = null;

    void Start()
    {
        instance = this;
    }

    public void addRemainTime(string _key, float time)
    {
        for (int i = 0; i < liTimeListener.Count; i++)
        {
            if (liTimeListener[i].key == _key)
            {
                liTimeListener[i].lastTime += time;
                liTimeListener[i].defaultTime += time;
                break;
            }
        }
    }

    public float getRemainTime(string _key)
    {
        float result = 0f;
        for (int i = 0; i < liTimeListener.Count; i++)
        {
            if (liTimeListener[i].key == _key)
            {
                result = liTimeListener[i].lastTime;
                break;
            }
        }
        return result;
    }

    public void addTimeEventListener(TIMEER_TYPE type, float time, Action<float> _callback, string _key = "",
        Action _endAction = null, string _datetime = "", Action<TimeSpan> _callback2 = null, bool isgoaltype = false)
    {
        if (isgoaltype == true)
        {
            DateTime datetime = Convert.ToDateTime(_datetime);

            GoalTimeData timedata = new GoalTimeData();
            timedata.key = _key;
            timedata.type = type;
            timedata.defaultTime = datetime;
            timedata.lastTime = new TimeSpan();
            timedata.callback = _callback2;
            if (_endAction != null)
            {
                timedata.endCallBack = _endAction;
            }


            bool iscreate = true;
            for (int i = 0; i < liGoalTimeListener.Count; i++)
            {
                if (liGoalTimeListener[i].key.Equals(_key))
                {
                    liGoalTimeListener[i] = timedata;
                    iscreate = false;
                    break;
                }
                else
                {
                    iscreate = true;
                }
            }

            if (iscreate)
            {
                liGoalTimeListener.Add(timedata);
            }

            if (goalTimeCoroutine == null)
            {
                goalTimeCoroutine = StartCoroutine(onPlayGoalTimeUpdate());
            }
        }
        else
        {
            TimeData timedata = new TimeData();
            timedata.key = _key;
            timedata.type = type;
            timedata.defaultTime = time;
            timedata.lastTime = time;
            timedata.callback = _callback;

            if (_endAction != null)
            {
                timedata.endCallBack = _endAction;
            }

            bool iscreate = true;
            for (int i = 0; i < liTimeListener.Count; i++)
            {
                if (liTimeListener[i].key.Equals(_key))
                {
                    liTimeListener[i] = timedata;
                    iscreate = false;
                    break;
                }
                else
                {
                    iscreate = true;
                }
            }

            if (iscreate)
            {
                liTimeListener.Add(timedata);
            }

            if (timeCoroutine == null)
            {
                timeCoroutine = StartCoroutine(onPlayTimeUpdate());
            }
        }

    }

    IEnumerator onPlayGoalTimeUpdate()
    {
        while (liGoalTimeListener.Count > 0)
        {
            yield return CGameUtils.WaitForSeconds(1f);
            for (int i = 0; i < liGoalTimeListener.Count; i++)
            {
                DateTime goal = Convert.ToDateTime(liGoalTimeListener[i].defaultTime);

                liGoalTimeListener[i].lastTime = goal - CGameUtils.getGoalTime(0);

                if (liGoalTimeListener[i].callback != null)
                {
                    liGoalTimeListener[i].callback(liGoalTimeListener[i].lastTime);
                }

                if (liGoalTimeListener[i].lastTime.TotalSeconds <= 0)
                {
                    if (liGoalTimeListener[i].endCallBack != null)
                    {
                        liGoalTimeListener[i].endCallBack();
                    }

                    if (liGoalTimeListener[i].type == TIMEER_TYPE.REMOVE)
                    {
                        removeGoladTimeEventListener(liGoalTimeListener[i]);
                    }
                }
            }
        }
    }

    IEnumerator onPlayTimeUpdate()
    {
        while (liTimeListener.Count > 0)
        {
            yield return CGameUtils.WaitForSeconds(1f);
            for (int i = 0; i < liTimeListener.Count; i++)
            {
                liTimeListener[i].lastTime -= 1f;
                if (liTimeListener[i].callback != null)
                {
                    liTimeListener[i].callback(liTimeListener[i].lastTime);
                }
                if (liTimeListener[i].lastTime <= 0)
                {
                    if (liTimeListener[i].endCallBack != null)
                    {
                        liTimeListener[i].endCallBack();
                    }

                    if (liTimeListener[i].type == TIMEER_TYPE.REMOVE)
                    {
                        removeTimeEventListener(liTimeListener[i]);
                    }
                    else
                    {
                        liTimeListener[i].lastTime = liTimeListener[i].defaultTime;
                    }
                }
            }
        }
    }

    public void onStopTimeEventListener(string _key)
    {
        for (int i = 0; i < liTimeListener.Count; i++)
        {
            if (liTimeListener[i].key.Equals(_key))
            {
                liTimeListener.Remove(liTimeListener[i]);
                break;
            }
        }
    }

    public void removeTimeEventListener(TimeData _data)
    {
        liTimeListener.Remove(_data);
        if (liTimeListener.Count <= 0)
        {
            StopCoroutine(timeCoroutine);
            timeCoroutine = null;
        }
    }


    public void removeGoladTimeEventListener(GoalTimeData _data)
    {
        liGoalTimeListener.Remove(_data);
        if (liGoalTimeListener.Count <= 0)
        {
            StopCoroutine(goalTimeCoroutine);
            goalTimeCoroutine = null;
        }
    }
}
