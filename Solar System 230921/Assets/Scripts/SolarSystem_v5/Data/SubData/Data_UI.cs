using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Data_UI : EssentialData, ISender, IReceiver
{
    private int nowOrbID = 0;
    public override int NowOrbID
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
                SendData(NowOrbData);
                SendData(NowOrbTrn);
            }
        }
    }

    private bool isMaster;
    public override bool IsMaster
    {
        get
        {
            isMaster = data.IsMaster;
            return isMaster;
        }
        set
        {
            if (isMaster != value)
            {
                isMaster = value;
                SendData(IsMaster);
            }
        }
    }

    private bool isSyncMode = true;
    public override bool IsSyncMode
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

    //===========================================================================

    private List<Dictionary<string, float>> orbDatas = new List<Dictionary<string, float>>();
    public List<Dictionary<string, float>> OrbDatas
    {
        get
        {
            orbDatas = data.converter.FromOrbDatasToUIDatas(data.OrbDatas);
            return orbDatas;
        }
        set 
        {
            if (orbDatas != value)
            {
                orbDatas = value;
                data.OrbDatas = data.converter.FromUIDatasToOrbDatas(orbDatas, data.OrbDatas);
            }
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
            // 데이터가 같은데 업데이트 하고 있음
            // 딕셔너리 비교용 함수 필요
            if (nowOrbData.Values != value.Values)
            {
                nowOrbData = value;
                orbDatas[NowOrbID] = nowOrbData;
                data.OrbDatas = data.converter.FromUIDatasToOrbDatas(orbDatas, data.OrbDatas);

                SendData(NowOrbID);
                SendData(nowOrbData);
            }
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

    public void SendData<T>(T data)
    {
        foreach (IReceiver r in receivers)
        {
            //Debug.Log("send message : " + GetType().Name + " -> " + r.GetType().Name);
            r.ReceiveData(data);
        }
    }

    public void ReceiveData<T>(T _data)
    {
        if (_data is int)
        {
            NowOrbID = (int)(object)_data;
        }
        if (_data is Dictionary<string, float>)
        {
            NowOrbData = _data as Dictionary<string, float>;
        }
    }
}