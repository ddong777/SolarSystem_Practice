using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    private UnityAction OnTimeDone;

    private bool timerSet = false;

    private float timeCount = 0;
    private float timeValue = 0;

    public void InitTimer(int count, bool isAccessible)
    {
        timeCount = count;
        timerSet = isAccessible;
    }

    public void ResetTimer()
    {
        timeValue = timeCount;
    }

    public void AddFunction(UnityAction _func)
    {
        if (!timerSet)
        {
            return;
        }
        OnTimeDone += _func;
    }

    public void RunTimer()
    {
        if (!timerSet)
        {
            return;
        }

        if (timeValue >= 0)
        {
            timeValue -= Time.deltaTime;
        } else
        {
            OnTimeDone.Invoke();
            ResetTimer();
        }
    }
}
