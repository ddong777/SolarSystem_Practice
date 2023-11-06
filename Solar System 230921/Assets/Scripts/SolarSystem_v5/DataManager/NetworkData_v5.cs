using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkData_v5 : DataSender, IReceiver
{
    public bool isTestMode = true;
    public bool IsMaster = true;

    private Hashtable customProperties = new Hashtable() { 
        { PropertyKey.ID.ToString(), 0 },
        { PropertyKey.OrbData.ToString(), null },
        { PropertyKey.OrbID.ToString(), null },
        { PropertyKey.OrbPosList.ToString() , null },
        { PropertyKey.OrbRotList.ToString(), null },
    };

    public Hashtable CustomPropeties
    {
        get => customProperties;
        set
        {
            if (CustomPropeties == value)
            {
                return;
            }
            customProperties = value;
        }
    }

    public void SetIsMaster(bool _b)
    {
        if (IsMaster == _b)
        {
            return;
        }
        IsMaster = _b;
    }

    public void SetDataFromServer(Hashtable _hashtable = null)
    {
        if (_hashtable == null) // ������Ƽ�� ������� ��
        {
            CustomPropeties = new Hashtable() { { PropertyKey.ID.ToString(), 0 },
                                                { PropertyKey.OrbData.ToString(), null },
                                                { PropertyKey.OrbID.ToString(), null },
                                                { PropertyKey.OrbPosList.ToString() , null },
                                                { PropertyKey.OrbRotList.ToString(), null },
            };
        }
        else
        {
            CustomPropeties = _hashtable;
        }
    }

    public void ReceiveData(ISender _sender)
    {
        switch (_sender)
        {
            case SyncManager_v5 sender:
                Debug.Log(GetType().Name + "�� " + sender.GetType().Name + "�κ��� ������ ����");

                isTestMode = sender.isTestMode;
                SetIsMaster(sender.IsMasterClient);
                SetDataFromServer(sender.CustomPropeties);
                SendData();

                break;
            default:
                break;
        }
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
            Debug.Log($"{key}�� ���� Ű�� �Դϴ�.");
        }
        return (int)CustomPropeties[key.ToString()];
    }
    public string[] GetCustomProperty_stringArray(PropertyKey key)
    {
        if (!CustomPropeties.ContainsKey(key.ToString()))
        {
            Debug.Log($"{key}�� ���� Ű�� �Դϴ�.");
        }
        return (string[])CustomPropeties[key.ToString()];
    }
    public Vector3[] GetCustomProperty_Vec3Array(PropertyKey key)
    {
        if (!CustomPropeties.ContainsKey(key.ToString()))
        {
            Debug.Log($"{key}�� ���� Ű�� �Դϴ�.");
        }
        return (Vector3[])CustomPropeties[key.ToString()];
    }
}
