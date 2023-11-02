using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serialize : MonoBehaviour
{
    public string FromOrbDataToString(OrbData data)
    {
        return JsonUtility.ToJson(data);
    }

    public OrbData FromStringToOrbData(string stringData)
    {
        return JsonUtility.FromJson<OrbData>(stringData);
    }

    public string[] FromOrbDatasToJson(List<OrbData> orbDatas)
    {
        string[] jsonDatas = new string[orbDatas.Count];
        for (int i = 0; i < jsonDatas.Length; i++)
        {
            jsonDatas[i] = JsonUtility.ToJson(orbDatas[i]);
        }

        return jsonDatas;
    }

    public List<OrbData> FromJsonToOrbDatas(string[] _strings)
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
}
