using System;
using System.Collections.Generic;
using UnityEngine;

public class Converter : MonoBehaviour
{
    public List<Dictionary<string, float>> FromOrbDatasToUIDatas(List<OrbData> datas)
    {
        List<Dictionary<string, float>> dictionary = new List<Dictionary<string, float>>();
        foreach(OrbData data in datas)
        {
            dictionary.Add(FromOrbDataToUIData(data));
        }

        return dictionary;
    }

    public List<OrbData> FromUIDatasToOrbDatas(List<Dictionary<string, float>> uiDatas, List<OrbData> orbDatas)
    {
        for (int i = 0; i < orbDatas.Count; i++)
        {
            Dictionary<string, float> uiData = uiDatas[i];
            orbDatas[i] = FromUIDataToOrbData(uiData, orbDatas[i]);
        }

        return orbDatas;
    }

    public Dictionary<string, float> FromOrbDataToUIData(OrbData data)
    {
        Dictionary<string, float> dictionary = new Dictionary<string, float>(){
            { "id", data.id },
            { "isCenter", Convert.ToInt32(data.isCenterOrb)},
            { "orbType", (float)data.orbType },
            { "orbPosX", data.orbPosX },
            { "orbRotZ", data.orbRotZ },
            { "orbSize", data.orbSize },
            { "spinDir", (float)data.spinDir },
            { "spinSpeed", data.spinSpeed },
            { "orbitDir", (float)data.orbitDir },
            { "orbitSpeed", data.orbitSpeed }
        };

        return dictionary;
    }

    public OrbData FromUIDataToOrbData(Dictionary<string, float> data, OrbData orbData)
    {
        orbData.orbType = (OrbType)Enum.ToObject(typeof(OrbType), (int)data["orbType"]);
        orbData.orbPosX = data["orbPosX"];
        orbData.orbRotZ = data["orbRotZ"];
        orbData.orbSize = data["orbSize"];
        orbData.spinDir = (MoveDir)Enum.ToObject(typeof(MoveDir), (int)data["spinDir"]);
        orbData.spinSpeed = data["spinSpeed"];
        orbData.orbitDir = (MoveDir)Enum.ToObject(typeof(MoveDir), (int)data["orbitDir"]);
        orbData.orbitSpeed = data["orbitSpeed"];

        return orbData;
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
