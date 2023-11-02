using System.Collections.Generic;
using UnityEngine;

public class OrbDataManager_v4 : MonoBehaviour
{
    private SyncManager syncManager;

    public List<OrbData> orbDatas;
    public void Init()
    {
        syncManager = GameManager_v4.Inst.SyncManager;

        if (orbDatas.Count > 0) return;
        else
        {
            if (syncManager.IsMasterClient)
            {
                TextAsset textAsset = Resources.Load<TextAsset>("JSON/solarSystem_BasicData");
                OrbDataList dataList = JsonUtility.FromJson<OrbDataList>(textAsset.text);

                foreach (OrbData data in dataList.solarSystem_BasicData)
                {
                    orbDatas.Add(data);
                }
            }
            else
            {
                // 서버로부터 데이터 받아서 데이터세팅
                if (syncManager.GetCustomProperty_stringArray(PropertyKey.OrbData) != null)
                {
                    UpdateOrbDataFromServer(syncManager.GetCustomProperty_stringArray(PropertyKey.OrbData));
                }
            }
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

    public List<OrbData> JsonDeserialize(string[] _strings)
    {
        List<OrbData> datas = new List<OrbData>();

        string[] strings = _strings;

        foreach (string str in strings)
        {
            OrbData data = JsonUtility.FromJson<OrbData>(str);
            datas.Add(data);
        }

        return datas;
    }

    public void UpdateOrbDataFromServer(string[] _strings)
    {
        orbDatas = JsonDeserialize(_strings);
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
