using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemData_v5 : MonoBehaviour, ISender
{
    [SerializeField]
    private List<OrbData> orbDatas = new List<OrbData>();
    public List<OrbData> OrbDatas
    {
        get => orbDatas;
        set
        {
            if (orbDatas != value)
            {
                orbDatas = value;
                SendData();
            }
        }
    }

    public void Set()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("JSON/solarSystem_BasicData");
        OrbDataList dataList = JsonUtility.FromJson<OrbDataList>(textAsset.text);

        foreach (OrbData data in dataList.solarSystem_BasicData)
        {
            orbDatas.Add(data);
        }

        SendData();
    }

    public void Set(List<OrbData> _datas)
    {
        if (_datas == null) return;
        OrbDatas = _datas;
    }

    public void SetData(int _id, OrbData data)
    {
        OrbDatas[_id] = data;
    }

    //==============================================

    public List<IReceiver> receivers = new List<IReceiver>();

    public void Attach(IReceiver ob)
    {
        Debug.Log("attach");
        receivers.Add(ob);
    }

    public void Detach(IReceiver ob)
    {
        receivers.Remove(ob);
    }

    public void SendData()
    {
        foreach (IReceiver receiver in receivers)
        {
            receiver.ReceiveData(this);
        }
    }
}
