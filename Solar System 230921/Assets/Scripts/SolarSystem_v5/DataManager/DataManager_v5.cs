using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager_v5 : MonoBehaviour, IReceiver
{
    public SolarSystemData_v5 solarData;
    public UIData_v5 uidata;
    public NetworkData_v5 networkData;

    public void Init()
    {
        gameObject.AddComponent<SolarSystemData_v5>();
        gameObject.AddComponent<UIData_v5>();
        gameObject.AddComponent<NetworkData_v5>();

        solarData = GetComponent<SolarSystemData_v5>();
        uidata = GetComponent<UIData_v5>();
        networkData = GetComponent<NetworkData_v5>();
    }

    public void Set()
    {
        solarData.Set();
        // 이미 서버에 존재하는 데이터 있다면 가져와서 세팅 (임시)
        //if (networkData.OrbDatas() == null)
        //{
        //    solarData.Set();
        //} else
        //{
        //    solarData.Set(networkData.OrbDatas());
        //}
    }

    //==============================================

    public void ReceiveData(ISender sender)
    {

    }
}
