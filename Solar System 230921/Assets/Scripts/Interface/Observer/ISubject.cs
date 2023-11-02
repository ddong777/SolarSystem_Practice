using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject 
{
    public void Attach(IObserver ob);
    public void Detach(IObserver ob);
    public void Notify();
}
