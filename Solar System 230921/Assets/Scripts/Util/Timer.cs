using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public System.Action OnTimeDone;
    private float timeCount;
    private float timeValue;

    public void InitTimer(int count)
    {
        timeCount = count;
    }

    public void ResetTimer()
    {
        timeValue = timeCount;
    }

    public void RunTimer()
    {
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
