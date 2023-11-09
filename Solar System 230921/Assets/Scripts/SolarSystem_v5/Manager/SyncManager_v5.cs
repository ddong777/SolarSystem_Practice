using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class SyncManager_v5 : MonoBehaviourPunCallbacks
{
    private Data_Network data;

    public bool isTestMode = true;
    public bool StartInOfflineMode => PhotonNetwork.PhotonServerSettings.StartInOfflineMode;
    public bool IsMasterClient => PhotonNetwork.IsMasterClient;

    public Hashtable CustomPropeties {
        get
        {
            if (PhotonNetwork.CurrentRoom == null)
            {
                return null;
            }
            else
            {
                return PhotonNetwork.CurrentRoom.CustomProperties;
            }
        }
        set
        {
            if (value != null && value != PhotonNetwork.CurrentRoom.CustomProperties)
            {
                PhotonNetwork.CurrentRoom.SetCustomProperties(value);
            }
        }
    }

    private Timer timer = new Timer();

    public void Init()
    {
        data = FindObjectOfType<Data_Network>();
        data.Init();

        timer.InitTimer(10);
        timer.OnTimeDone += () => { 
            // 위치 동기화
        };
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        CustomPropeties = propertiesThatChanged; 
    }
}
