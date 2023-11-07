using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_UI : AData, ISender
{
    private Dictionary<string, float> nowOrbData = new Dictionary<string, float>()
    {
        { "id", 0f },
        { "orbType", 0f },
        { "orbPosX", 0f },
        { "orbRotZ", 0f },
        { "orbSize", 0f },
        { "spinDir", 0f },
        { "spinSpeed", 0f },
        { "orbitDir", 0f },
        { "orbitSpeed", 0f }
    };

    public Dictionary<string, float> NowOrbData
    {
        get
        {
            nowOrbData = converter.FromOrbDataToUIData(data.OrbDatas[NowOrbID]);
            return nowOrbData;
        }
        set 
        {
            if (value != nowOrbData)
            {
                nowOrbData = value;
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
            return data.OrbTrns[NowOrbID]; 
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