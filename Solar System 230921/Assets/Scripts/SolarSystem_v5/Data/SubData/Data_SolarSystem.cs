using System;
using System.Collections.Generic;
using UnityEngine;

public class Data_SolarSystem : AData
{
    private List<OrbData> orbDatas = new List<OrbData>();
    public List<OrbData> OrbDatas
    {
        get
        {
            orbDatas = data.OrbDatas;
            return orbDatas;
        }
        set
        {
            if (orbDatas != value)
            {
                orbDatas = value;
                data.OrbDatas = orbDatas;
            }
        }
    }

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
                data.OrbTrns = orbTrns;
            }
        }
    }

    public override void Init()
    {
        if (data == null)
        {
            data = FindObjectOfType<DataContainer>();
        }
        if (orbDatas.Count <= 0)
        {
            TextAsset textAsset = Resources.Load<TextAsset>("JSON/solarSystem_BasicData");
            OrbDataList dataList = JsonUtility.FromJson<OrbDataList>(textAsset.text);

            // 매번 자전/공전 속도 랜덤으로
            foreach (OrbData data in dataList.solarSystem_BasicData)
            {
                if (data.spinSpeed <= 0)
                {
                    data.spinSpeed = UnityEngine.Random.Range(10f, 100f);
                }

                if (data.orbitSpeed <= 0)
                {
                    data.orbitSpeed = UnityEngine.Random.Range(0.2f, 1f);
                }
            }

            OrbDatas = dataList.solarSystem_BasicData;
        }
    }

    public void SetData(int _id, OrbData _data)
    {
        if (_data == null)
        {
            return;
        }

        OrbDatas[_id] = _data;
    }

    public void Set(List<Transform> trns)
    {
        OrbTrns = trns;
    }
}
