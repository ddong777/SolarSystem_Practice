using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EssentialData : MonoBehaviour 
{
    protected DataContainer data;

    public virtual bool IsMaster { get; set; }

    public virtual bool IsSyncMode { get; set; }

    public virtual int NowOrbID { get; set; }

    public virtual void Init() { }

    public virtual void UpdateData() { }
}
