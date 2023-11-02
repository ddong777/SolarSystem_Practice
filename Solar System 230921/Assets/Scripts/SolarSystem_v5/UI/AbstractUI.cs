using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractUI : MonoBehaviour, ISubject
{
    public List<IObserver> observers = new List<IObserver>();

    public System.Action OnChangeData; // UI 상 데이터 변경점 (내부영향)
    public System.Action OnUpdateData; // UI 데이터 업데이트 (외부영향)

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
