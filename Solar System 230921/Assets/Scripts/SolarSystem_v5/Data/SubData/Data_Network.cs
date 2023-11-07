using System;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Data_Network : AData
{
    private bool isMaster = true;
    public bool IsMaster
    {
        get 
        { 
            return isMaster; 
        }
        set 
        { 
            if (isMaster != value)
            {
                isMaster = value;
                data.IsMaster = true;
            }
        }
    }

    private bool isSyncMode = true;
    public bool IsSyncMode
    {
        get
        {
            isSyncMode = data.IsSyncMode;
            return isSyncMode;
        }
    }

    private Hashtable customProperties = new Hashtable() { 
        { PropertyKey.ID.ToString(), 0 },
        { PropertyKey.OrbData.ToString(), null },
        { PropertyKey.OrbID.ToString(), null },
        { PropertyKey.OrbPosList.ToString() , null },
        { PropertyKey.OrbRotList.ToString(), null },
    };

    public Hashtable CustomPropeties
    {
        get
        {
            switch (customProperties[PropertyKey.ID.ToString()])
            {
                case 1:
                    SetCustomProperty(PropertyKey.OrbData, converter.FromOrbDatasToJson(data.OrbDatas));
                    break;
                case 2:
                    SetCustomProperty(PropertyKey.OrbID, data.NowOrbID);
                    break;
                case 3:
                    
                    Vector3[] _pos = new Vector3[data.OrbTrns.Count];
                    Vector3[] _rot = new Vector3[data.OrbTrns.Count];

                    for (int i = 0; i < data.OrbTrns.Count; i++)
                    {
                        _pos[i] = data.OrbTrns[i].transform.localPosition;
                        _rot[i] = data.OrbTrns[i].transform.localRotation.eulerAngles;
                    }
                    SetCustomProperty(PropertyKey.OrbPosList, _pos);
                    SetCustomProperty(PropertyKey.OrbRotList, _rot);

                    break;
                default:
                    break;
            }

            return customProperties;
        }

        set
        {
            if (customProperties != value && value != null)
            {
                customProperties = value;

                if (!isMaster)
                {
                    data.OrbDatas = converter.FromJsonToOrbDatas(GetCustomProperty_stringArr(PropertyKey.OrbData));
                    data.NowOrbID = GetCustomProperty_Int(PropertyKey.OrbID);
                    for (int i = 0; i < data.OrbTrns.Count; i++)
                    {
                        Transform trn = data.OrbTrns[i];
                        Vector3 pos = GetCustomProperty_Vec3Array(PropertyKey.OrbPosList)[i];
                        Vector3 rot = GetCustomProperty_Vec3Array(PropertyKey.OrbRotList)[i];
                        trn.localPosition = pos;
                        trn.localRotation = Quaternion.Euler(rot);
                    }
                }
            }
        }
    }

    public void Init(bool _isMaster, Hashtable _properties)
    {
        IsMaster = _isMaster;
        CustomPropeties = _properties;
    }

    //===============================================================================
    public void SetCustomProperty(PropertyKey key, int _id)
    {
        CustomPropeties[key.ToString()] = _id;
    }
    public void SetCustomProperty(PropertyKey key, string[] _strings)
    {
        CustomPropeties[key.ToString()] = _strings;
    }
    public void SetCustomProperty(PropertyKey key, Vector3[] _vec3)
    {
        CustomPropeties[key.ToString()] = _vec3;
    }

    public int GetCustomProperty_Int(PropertyKey key)
    {
        if (!CustomPropeties.ContainsKey(key.ToString()))
        {
            Debug.Log($"{key}는 없는 키값 입니다.");
        }
        return (int)CustomPropeties[key.ToString()];
    }
    public string[] GetCustomProperty_stringArr(PropertyKey key)
    {
        if (!CustomPropeties.ContainsKey(key.ToString()))
        {
            Debug.Log($"{key}는 없는 키값 입니다.");
        }
        return (string[])CustomPropeties[key.ToString()];
    }
    public Vector3[] GetCustomProperty_Vec3Array(PropertyKey key)
    {
        if (!CustomPropeties.ContainsKey(key.ToString()))
        {
            Debug.Log($"{key}는 없는 키값 입니다.");
        }
        return (Vector3[])CustomPropeties[key.ToString()];
    }
}
