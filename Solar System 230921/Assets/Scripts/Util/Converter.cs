using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Converter : MonoBehaviour
{
    public OrbData OrbToOrbData(Orb_v5 orb)
    {
        return orb.data;
    }
    public Dictionary<string, Vector3> OrbToPosNRot(Orb_v5 orb)
    {
        return new Dictionary<string, Vector3>() 
        { 
            {"position", orb.Position}, 
            {"rotation",  orb.Rotation} 
        };
    }

    public Dictionary<string, float> OrbDataToEditorData(OrbData data, Dictionary<string, float> editorData)
    {
        editorData["id"] = data.id;
        editorData["orbType"] = (float)data.orbType;
        editorData["orbPosX"] = data.orbPosX;
        editorData["orbRotZ"] = data.orbRotZ;
        editorData["orbSize"] = data.orbSize;
        editorData["spinDir"] = (float)data.spinDir;
        editorData["spinSpeed"] = data.spinSpeed;
        editorData["orbitDir"] = (float)data.orbitDir;
        editorData["orbitSpeed"] = data.orbitSpeed;

        return editorData;
    }

    public OrbData EditorDataToOrbData(Dictionary<string, float> editorData, OrbData data)
    {
        data.orbType = (OrbType)Enum.ToObject(typeof(OrbType), editorData["orbType"]); ;
        data.orbPosX = editorData["orbPosX"];
        data.orbRotZ = editorData["orbRotZ"];
        data.orbSize = editorData["orbSize"];
        data.spinDir = (MoveDir)Enum.ToObject(typeof(MoveDir), editorData["spinDir"]);
        data.spinSpeed = editorData["spinSpeed"];
        data.orbitDir = (MoveDir)Enum.ToObject(typeof(MoveDir), editorData["orbitDir"]);
        data.orbitSpeed = editorData["orbitSpeed"];

        return data;
    }

    public string[] OrbTypeString(List<OrbData> datas)
    {
        string[] strings = new string[datas.Count];
        for (int i = 0; i < datas.Count; i++)
        {
            strings[i] = datas[i].orbType.ToString();
        }
        return strings;
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
