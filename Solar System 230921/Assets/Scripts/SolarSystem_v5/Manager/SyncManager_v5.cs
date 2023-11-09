using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class SyncManager_v5 : MonoBehaviourPunCallbacks
{
    private Data_Network data;

    public bool StartInOfflineMode => PhotonNetwork.PhotonServerSettings.StartInOfflineMode;
    public bool IsMasterClient => PhotonNetwork.IsMasterClient;

    private Timer timer = new Timer();

    public void Init()
    {
        Debug.Log($"Player GUID : {PhotonNetwork.AuthValues.UserId}");
        data = FindObjectOfType<Data_Network>();
        data.Init();

        timer.InitTimer(10);
        timer.OnTimeDone += () => { 
            // ��ġ ����ȭ �Լ�
        };
    }

    public void Set()
    {
        SetCustomProperties();
    }

    public void SetCustomProperties()
    {
        if (PhotonNetwork.CurrentRoom == null)
        {
            Debug.Log("CurrentRoom �� �����ϴ�.");
            return;
        }
        if (IsMasterClient)
        {
            Debug.Log("Master Client: ������ ������ ����");
            PhotonNetwork.CurrentRoom.SetCustomProperties(data.CustomPropeties);
        }
        
        if (!IsMasterClient)
        {
            Debug.Log("Client: ���� ������ Ȯ��");
            data.CustomPropeties = PhotonNetwork.CurrentRoom.CustomProperties;
        }
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (!IsMasterClient)
        {
            data.CustomPropeties = propertiesThatChanged;
        }
    }
}
