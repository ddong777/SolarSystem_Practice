using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class SyncManager_v5 : MonoBehaviourPunCallbacks
{
    private Timer timer = new Timer();
    private Data_Network data;

    public bool IsMasterClient => PhotonNetwork.IsMasterClient;

    private void LateUpdate()
    {
        timer.RunTimer();
    }

    public void Init()
    {
        Debug.Log($"Player GUID : {PhotonNetwork.AuthValues.UserId}");
        data = FindObjectOfType<Data_Network>();
        data.Init(IsMasterClient);

        timer.InitTimer(10, IsMasterClient);

        timer.AddFunction(()=> { Debug.Log("������ Ŭ���̾�Ʈ ��ġ ����ȭ"); });
        timer.AddFunction(()=> { PhotonNetwork.CurrentRoom.SetCustomProperties(data.SetCustomPropertiesPosNRot()); });
    }

    public void Set()
    {
        if (PhotonNetwork.CurrentRoom == null)
        {
            Debug.Log("CurrentRoom�� �����ϴ�.");
            return;
        }
    }
    public void OnMasterClientChange()
    {
        data.IsMaster = IsMasterClient;
    }

    public void Send_FromMaster()
    {
        if (IsMasterClient &&
            data.CustomPropeties.ContainsKey(PropertyKey.ID.ToString()))
        {
            //Debug.Log("���� ������ ����: " + data.CustomPropeties);
            data.SetCustomProperty(PropertyKey.ID, 0);
            PhotonNetwork.CurrentRoom.SetCustomProperties(data.CustomPropeties);
            timer.ResetTimer();
        }
    }

    public void SendFromClient()
    {
        if (!IsMasterClient &&
            data.CustomPropeties.ContainsKey(PropertyKey.ID.ToString()))
        {
            data.SetCustomProperty(PropertyKey.ID, 1);
            PhotonNetwork.CurrentRoom.SetCustomProperties(data.CustomPropeties);
        }
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (!propertiesThatChanged.ContainsKey(PropertyKey.ID.ToString()))
        {
            // Ŀ���� ������Ƽ ���� �� �Ǿ������� �н�
            return;
        }

        if ((int)propertiesThatChanged[PropertyKey.ID.ToString()] == 0)
        {
            if (!IsMasterClient)
            {
                //Debug.Log("���� ������ ����: " + propertiesThatChanged);
                data.CustomPropeties = propertiesThatChanged;
            }
        } else if ((int)propertiesThatChanged[PropertyKey.ID.ToString()] == 1)
        {
            Send_FromMaster();
            data.SetCustomProperty(PropertyKey.ID, 0);
        }
    }
}
