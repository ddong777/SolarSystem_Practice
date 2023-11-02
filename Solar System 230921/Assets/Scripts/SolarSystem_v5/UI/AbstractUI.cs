using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractUI : MonoBehaviour, ISubject
{
    public List<IObserver> observers = new List<IObserver>();

    public System.Action OnChangeData; // UI �� ������ ������ (���ο���)
    public System.Action OnUpdateData; // UI ������ ������Ʈ (�ܺο���)

    public abstract void Init();

    public void Attach(IObserver ob)
    {
        observers.Add(ob);
    }

    public void Detach(IObserver ob)
    {
        observers.Remove(ob);
    }

    public void Notify()
    {
        foreach (IObserver ob in observers)
        {
            ob.OnNotify();
        }
    }
}
