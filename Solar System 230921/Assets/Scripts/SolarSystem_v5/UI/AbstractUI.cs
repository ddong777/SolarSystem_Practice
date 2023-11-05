using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractUI : MonoBehaviour, ISender
{
    public List<IReceiver> observers = new List<IReceiver>();

    public System.Action OnChangeData; // UI 상 데이터 변경점 (내부영향)
    public System.Action OnUpdateData; // UI 데이터 업데이트 (외부영향)

    public abstract void Init();

    public void Attach(IReceiver ob)
    {
        observers.Add(ob);
    }

    public void Detach(IReceiver ob)
    {
        observers.Remove(ob);
    }

    public void SendData()
    {
        foreach (IReceiver ob in observers)
        {
            //ob.ReceiveData();
        }
    }
}
