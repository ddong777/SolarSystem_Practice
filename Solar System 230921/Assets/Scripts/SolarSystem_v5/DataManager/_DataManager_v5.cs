using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _DataManager_v5 : MonoBehaviour, ISubject
{
    public List<IObserver> observers = new List<IObserver>();
    protected Serialize serialize = new Serialize();

    public void Attach(IObserver ob)
    {
        observers.Add(ob);
    }

    public void Detach(IObserver ob)
    {
        observers.Remove(ob);
    }

    public abstract void Notify();
}
