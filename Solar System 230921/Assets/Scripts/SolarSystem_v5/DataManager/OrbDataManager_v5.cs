using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbDataManager_v5 : _DataManager_v5
{
    public List<OrbData> orbDatas;

    public void Set()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("JSON/solarSystem_BasicData");
        OrbDataList dataList = JsonUtility.FromJson<OrbDataList>(textAsset.text);

        foreach (OrbData data in dataList.solarSystem_BasicData)
        { 
            orbDatas.Add(data);
        }
        Notify();
    }

    public void Set(List<OrbData> _datas)
    {
        orbDatas = _datas;
        Notify();
    }

    public OrbData GetData(int _id)
    {
        return orbDatas[_id];
    }

    public void SetData(int _id, OrbData data)
    {
        orbDatas[_id] = data;
        Notify(serialize.FromOrbDataToString(data));
    }

    public override void Notify()
    {
        Debug.Log("Orb Data Manager Update");
        foreach (_Manager_v5 ob in observers)
        {
            ob.OnNotify();
        }
    }

    public void Notify(string _data)
    {
        Debug.Log("Orb Data Manager Update");
        foreach (_Manager_v5 ob in observers)
        {
            ob.OnNotify(_data);
        }
    }
}
