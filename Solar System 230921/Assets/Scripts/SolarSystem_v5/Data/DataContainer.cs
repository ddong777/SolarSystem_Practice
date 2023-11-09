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
                eventManager.GetEvent("isMaster")?.Invoke();
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
                eventManager.GetEvent("isSyncMode")?.Invoke();
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
                eventManager.GetEvent("nowOrbID")?.Invoke();
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
            orbDatas = value;
            eventManager.GetEvent("orbDatas")?.Invoke();
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
                eventManager.GetEvent("orbTrns")?.Invoke();
            }
        }
    }
    public UnityAction OnOrbTrnsChange;

    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        converter = new Converter();
        eventManager = FindObjectOfType<EventManager_v5>();

        foreach (EssentialData sub in FindObjectsOfType(typeof(EssentialData)))
        {
            if (sub != this)
            {
                subDatas.Add(sub);
            }
        }
    }
}
