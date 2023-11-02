using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using Photon.Pun;
using System.Text;
using System.Linq;

public class OrbDataList
{
    public List<OrbData> solarSystem_BasicData;
}

public class OrbDataManager_v3 : MonoBehaviour 
{
    public List<OrbData> orbDatas;

    public void Init()
    {
        if (orbDatas.Count > 0) return;

        TextAsset textAsset = Resources.Load<TextAsset>("JSON/solarSystem_BasicData");
        OrbDataList dataList = JsonUtility.FromJson<OrbDataList>(textAsset.text);

        foreach (OrbData data in dataList.solarSystem_BasicData)
        {
            orbDatas.Add(data);
        }
    }

    public string[] JsonSerialize_CurrentData()
    {
        string[] jsonDatas = new string[orbDatas.Count];
        for (int i = 0; i < jsonDatas.Length; i++)
        {
            jsonDatas[i] = JsonUtility.ToJson(orbDatas[i]);
        }

        return jsonDatas;
    }

    public List<OrbData> JsonDeserialize()
    {
        List<OrbData> datas = new List<OrbData>();

        string[] temps = GameManager_v4.Inst.SyncManager.GetCustomProperty_stringArray(PropertyKey.OrbData);


        foreach (string temp in temps)
        {
            OrbData data = JsonUtility.FromJson<OrbData>(temp);
            datas.Add(data);
        }

        return datas;
    }

    public void UpdateOrbDataFromServer()
    {
        orbDatas = JsonDeserialize();
    }

    public void UpdateData(OrbData orb, int id)
    {
        orbDatas[id].id = orb.id;
        orbDatas[id].orbType = orb.orbType;
        orbDatas[id].orbPrefab = Resources.Load<GameObject>($"Prefabs/OrbFeature/{(int)orb.orbType}_{orb.orbType}Feature");
        orbDatas[id].orbPosX = orb.orbPosX;
        orbDatas[id].orbRotZ = orb.orbRotZ;
        orbDatas[id].orbSize = orb.orbSize;
        orbDatas[id].spinDir = orb.spinDir;
        orbDatas[id].spinSpeed = orb.spinSpeed;
        orbDatas[id].orbitDir = orb.orbitDir;
        orbDatas[id].orbitSpeed = orb.orbitSpeed;
    }

    public void RemoveData(int index)
    {
        orbDatas.RemoveAt(index);
    }

    // orb 세팅 기능
    public void SetOrb(OrbData orb, int id)
    {
        orb.id = orbDatas[id].id;
        orb.orbType = orbDatas[id].orbType;
        orb.orbPrefab = Resources.Load<GameObject>($"Prefabs/OrbFeature/{(int)orbDatas[id].orbType}_{orbDatas[id].orbType}Feature");
        orb.isCenterOrb = orbDatas[id].isCenterOrb;
        orb.orbPosX = orbDatas[id].orbPosX;
        orb.orbRotZ = orbDatas[id].orbRotZ;
        orb.orbSize = orbDatas[id].orbSize;
        orb.spinDir = orbDatas[id].spinDir;
        orb.spinSpeed = orbDatas[id].spinSpeed;
        orb.orbitDir = orbDatas[id].orbitDir;
        orb.orbitSpeed = orbDatas[id].orbitSpeed;
    }
}
