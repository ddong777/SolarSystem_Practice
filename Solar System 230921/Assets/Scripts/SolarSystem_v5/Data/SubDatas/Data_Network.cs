using System;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Data_Network : EssentialData
{
    private bool isMaster = true;
    public override bool IsMaster
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
                data.IsMaster = isMaster;
            }
        }
    }

    private bool isSyncMode = true;
    public override bool IsSyncMode
    {
        get
        {
            isSyncMode = data.IsSyncMode;
            return isSyncMode;
        }
    }

    //===========================================================================

    private Hashtable customProperties = new Hashtable() { 
        { PropertyKey.ID.ToString(), 0 },
        { PropertyKey.OrbID.ToString(), 0 },
        { PropertyKey.OrbData.ToString(), null },
        { PropertyKey.OrbPosList.ToString() , null },
        { PropertyKey.OrbRotList.ToString(), null },
    };

    public Hashtable CustomPropeties
    {
        get // ������ Ŭ���̾�Ʈ
        {
            // ������ Ŭ���̾�Ʈ�̸鼭, ī�޶� ��ũ �ѵξ��� ��쿡�� NowOrbID ������ ����
            if (IsMaster && IsSyncMode)
            {
                SetCustomProperty(PropertyKey.OrbID, data.NowOrbID);
            }

            SetCustomProperty(PropertyKey.OrbData, data.converter.FromOrbDatasToJson(data.OrbDatas));

            SetPosNRotData_FromMainData();

            return customProperties;
        }

        set // Ŭ���̾�Ʈ
        {
            if (customProperties != value && value.ContainsKey(PropertyKey.ID.ToString()))
            {
                customProperties = value;

                // Ŭ���̾�Ʈ�̸鼭, ī�޶� ��ũ �ѵξ��� ��쿡�� NowOrbID �����κ��� ����ȭ
                if (!IsMaster && IsSyncMode)
                {
                    data.NowOrbID = GetCustomProperty_Int(PropertyKey.OrbID);
                }

                data.OrbDatas = data.converter.FromJsonToOrbDatas(GetCustomProperty_stringArr(PropertyKey.OrbData));

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

    //===================================================================

    public void Init(bool _isMaster)
    {
        data = FindObjectOfType<DataContainer>();

        IsMaster = _isMaster;
    }

    private void SetPosNRotData_FromMainData()
    {
        Vector3[] _pos = new Vector3[data.OrbTrns.Count];
        Vector3[] _rot = new Vector3[data.OrbTrns.Count];

        for (int i = 0; i < data.OrbTrns.Count; i++)
        {
            if (data.OrbTrns[i] == null)
            {
                return;
            }
            _pos[i] = data.OrbTrns[i].transform.localPosition;
            _rot[i] = data.OrbTrns[i].transform.localRotation.eulerAngles;
        }
        SetCustomProperty(PropertyKey.OrbPosList, _pos);
        SetCustomProperty(PropertyKey.OrbRotList, _rot);
    }
    public Hashtable SetCustomPropertiesPosNRot()
    {
        SetPosNRotData_FromMainData();

        return customProperties;
    }

    //===============================================================================
    public void SetCustomProperty(PropertyKey key, int _id)
    {
        customProperties[key.ToString()] = _id;
    }
    public void SetCustomProperty(PropertyKey key, string[] _strings)
    {
        customProperties[key.ToString()] = _strings;
    }
    public void SetCustomProperty(PropertyKey key, Vector3[] _vec3)
    {
        customProperties[key.ToString()] = _vec3;
    }

    public int GetCustomProperty_Int(PropertyKey key)
    {
        if (!customProperties.ContainsKey(key.ToString()))
        {
            Debug.Log($"{key}�� ���� Ű�� �Դϴ�.");
        }
        return (int)customProperties[key.ToString()];
    }
    public string[] GetCustomProperty_stringArr(PropertyKey key)
    {
        if (!customProperties.ContainsKey(key.ToString()))
        {
            Debug.Log($"{key}�� ���� Ű�� �Դϴ�.");
        }
        return (string[])customProperties[key.ToString()];
    }
    public Vector3[] GetCustomProperty_Vec3Array(PropertyKey key)
    {
        if (!customProperties.ContainsKey(key.ToString()))
        {
            Debug.Log($"{key}�� ���� Ű�� �Դϴ�.");
        }
        return (Vector3[])customProperties[key.ToString()];
    }
}
