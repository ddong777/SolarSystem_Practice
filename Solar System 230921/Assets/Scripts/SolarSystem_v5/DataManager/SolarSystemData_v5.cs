using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemData_v5 : DataSender
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

    private void Set()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("JSON/solarSystem_BasicData");
        OrbDataList dataList = JsonUtility.FromJson<OrbDataList>(textAsset.text);

        OrbDatas = dataList.solarSystem_BasicData;
    }

    public void Set(List<OrbData> _datas)
    {
        if (_datas == null)
        {
            Set();
            return;
        }
        else if (_datas == OrbDatas)
        {
            return;
        }

        OrbDatas = _datas;
    }

    public void SetData(int _id, OrbData _data)
    {
        if (_data == null)
        {
            return;
        }

        OrbDatas[_id] = _data;
    }
}
