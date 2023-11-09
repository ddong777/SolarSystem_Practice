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
            // 위치 동기화 함수
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
            Debug.Log("CurrentRoom 이 없습니다.");
            return;
        }
        if (IsMasterClient)
        {
            Debug.Log("Master Client: 서버로 데이터 전송");
            PhotonNetwork.CurrentRoom.SetCustomProperties(data.CustomPropeties);
        }
        
        if (!IsMasterClient)
        {
            Debug.Log("Client: 서버 데이터 확인");
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
