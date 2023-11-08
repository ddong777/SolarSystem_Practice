using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataContainer : EssentialData
{
    public Converter converter;
    public List<EssentialData> subDatas = new List<EssentialData>();

    private EventManager_v5 eventManager;

    private bool isMaster = true;
    public override bool IsMaster
    {
        get { return isMaster; }
        set 
        {
            if (value != isMaster)
            {
                isMaster = value;
                foreach (EssentialData sub in subDatas)
                {
                    sub.IsMaster = isMaster;
                }
                OnIsMasterChange?.Invoke();
            }
        }
    }

    private bool isSyncMode = true;
    public override bool IsSyncMode
    {
        get { return isSyncMode; }
        set 
        {
            if (value != isSyncMode)
            {
                isSyncMode = value;
                foreach (EssentialData sub in subDatas)
                {
                    sub.IsSyncMode = isSyncMode;
                }
                OnSyncModeChange?.Invoke();
            }
        }
    }

    [SerializeField]
    private int nowOrbID = 0;
    public override int NowOrbID
    {
        get { return nowOrbID; }
        set 
        {
            if (value != nowOrbID)
            {
                nowOrbID = value;
                foreach (EssentialData sub in subDatas)
                {
                    sub.NowOrbID = nowOrbID;
                }
                OnNowOrbIDChange?.Invoke();
            }
        }
    }

    //===========================================================================

    private List<OrbData> orbDatas = new List<OrbData>();
    public List<OrbData> OrbDatas
    {
        get 
        {
            return orbDatas; 
        }
        set 
        { 
            if (orbDatas != value)
            {
                orbDatas = value;
                OnOrbDatasChange?.Invoke();
            }
        }
    }
    public UnityAction OnOrbDatasChange;


    private List<Transform> orbTrns = new List<Transform>();
    public List<Transform> OrbTrns
    {
        get 
        { 
            return orbTrns; 
        }
        set 
        {
            if (orbTrns != value)
            {
                orbTrns = value;
                OnOrbTrnsChange?.Invoke();
            }
        }
    }
    public UnityAction OnOrbTrnsChange;

    public override void Init()
    {
        converter = new Converter();

        foreach (EssentialData sub in FindObjectsOfType(typeof(EssentialData)))
        {
            if (sub != this)
            {
                subDatas.Add(sub);
            }
        }
    }

    // 한번만 실행해야 함...
    public void Set()
    {
        eventManager = FindObjectOfType<EventManager_v5>();

        eventManager.SetEvent("isMaster", OnIsMasterChange);
        eventManager.SetEvent("isSyncMode", OnSyncModeChange);
        eventManager.SetEvent("nowOrbID", OnNowOrbIDChange);
        eventManager.SetEvent("orbDatas", OnOrbDatasChange);
        eventManager.SetEvent("orbTrns", OnOrbTrnsChange);
    }
}
