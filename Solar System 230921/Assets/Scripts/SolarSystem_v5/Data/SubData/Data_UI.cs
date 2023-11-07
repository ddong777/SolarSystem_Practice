using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_UI : AData, ISender
{
    private List<Dictionary<string, float>> orbDatas = new List<Dictionary<string, float>>();
    public List<Dictionary<string, float>> OrbDatas
    {
        get
        {
            orbDatas = converter.FromOrbDatasToUIDatas(data.OrbDatas);
            return orbDatas;
        }
    }

    private Dictionary<string, float> nowOrbData = new Dictionary<string, float>();

    public Dictionary<string, float> NowOrbData
    {
        get
        {
            nowOrbData = OrbDatas[NowOrbID];
            return nowOrbData;
        }
        set 
        {
            if (value != nowOrbData)
            {
                nowOrbData = value;
                orbDatas[NowOrbID] = nowOrbData;
                data.OrbDatas[NowOrbID] = converter.FromUIDataToOrbData(nowOrbData);
            }
        }
    }

    private int nowOrbID = 0;
    public int NowOrbID
    {
        get
        {
            nowOrbID = data.NowOrbID;
            return nowOrbID;
        }
        set
        {
            if (nowOrbID != value)
            {
                nowOrbID = value;
                data.NowOrbID = nowOrbID;
            }
        }
    }

    public bool IsMaster
    {
        get
        {
            return data.IsMaster;
        }
    }

    private bool isSyncMode = true;
    public bool IsSyncMode
    {
        get 
        { 
            return isSyncMode; 
        } 
        set
        {
            isSyncMode = value;
            data.IsSyncMode = isSyncMode;
        }
    }

    public Transform NowOrbTrn
    {
        get 
        {
            if (data.OrbTrns.Count > NowOrbID)
            {
                return data.OrbTrns[NowOrbID];
            }
            else
            {
                Debug.Log("Orb Trn List 비어있음.");
                return null;
            }
        }
    }

    //===================================================================
    
    private List<IReceiver> receivers = new List<IReceiver>();

    public void Attach(IReceiver receiver)
    {
        receivers.Add(receiver);
    }

    public void Detach(IReceiver receiver)
    {
        receivers.Remove(receiver);
    }

    public void SendData()
    {
        foreach (IReceiver r in receivers)
        {
        }
    }
}