using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Converter : MonoBehaviour
{
    public Dictionary<string, float> FromOrbDataToUIData(OrbData data)
    {
        Dictionary<string, float> dictionary = new Dictionary<string, float>(){
            { "id", data.id },
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

    public OrbData FromUIDataToOrbData(Dictionary<string, float> data)
    {
        OrbData orbData = new OrbData()
        {
            id = (int)data["id"],
            orbType = (OrbType)Enum.ToObject(typeof(OrbType), data["orbType"]),
            orbPosX = data["orbPosX"],
            orbRotZ = data["orbRotZ"],
            orbSize = data["orbSize"],
            spinDir = (MoveDir)Enum.ToObject(typeof(MoveDir), data["spinDir"]),
            spinSpeed = data["spinSpeed"],
            orbitDir = (MoveDir)Enum.ToObject(typeof(MoveDir), data["orbitDir"]),
            orbitSpeed = data["orbitSpeed"]
        };
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
