using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager_v5 : MonoBehaviourPunCallbacks
{
    private SyncManager_v5 syncManager;

    public bool isMaster = PhotonNetwork.IsMasterClient;
}
