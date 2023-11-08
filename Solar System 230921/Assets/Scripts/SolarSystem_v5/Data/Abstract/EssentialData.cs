using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EssentialData : MonoBehaviour 
{
    protected DataContainer data;

    public virtual bool IsMaster { get; set; }
    public UnityAction OnIsMasterChange;

    public virtual bool IsSyncMode { get; set; }
    public UnityAction OnSyncModeChange;

    public virtual int NowOrbID { get; set; }
    public UnityAction OnNowOrbIDChange;

    public virtual void Init()
    {
        data = FindObjectOfType<DataContainer>();
        data.Init();
    }
}
