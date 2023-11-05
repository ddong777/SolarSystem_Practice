using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractUI : MonoBehaviour, ISender
{
    public List<IReceiver> observers = new List<IReceiver>();

    public System.Action OnChangeData; // UI �� ������ ������ (���ο���)
    public System.Action OnUpdateData; // UI ������ ������Ʈ (�ܺο���)

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
