using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager_v5 : MonoBehaviourPunCallbacks
{
    private SyncManager_v5 syncManager;

    private string gameVersion = "1";
    private string playerNamePrefKey = "player";
    private bool isConnecting;

    [SerializeField]
    private byte maxPlayerPerRoom = 4;

    private void Awake()
    {
        NetworkManager[] objs = FindObjectsOfType(typeof(NetworkManager)) as NetworkManager[];
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }

        Init();
    }

    private void Init()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            SetPlayerName();
        }
        else
        {
            syncManager = FindObjectOfType<SyncManager_v5>();
            syncManager.isTestMode = false;
        }
    }

    private void SetPlayerName()
    {
        if (PlayerPrefs.HasKey(playerNamePrefKey))
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString(playerNamePrefKey);
        }
        else
        {
            string name = $"player_{Random.Range(0, 10)}";
            PhotonNetwork.NickName = name;
            PlayerPrefs.SetString(playerNamePrefKey, name);
        }

        Debug.Log($"Player Name : {PhotonNetwork.NickName}");
    }

    public void Connect()
    {
        isConnecting = true;

        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // ���� ���߿� ����
    public void LoadSpace()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("������ Ŭ���̾�Ʈ�� �ƴ�");
            return;
        }

        Debug.LogFormat("���� �÷��̾� �� : {0}", PhotonNetwork.CurrentRoom.PlayerCount);

        // �÷��̾� ���� �� ���� ������ Ŭ���̾�Ʈ ������Ʈ
        // PhotonNetwork.LoadLevel("SolarSystem_v4"); 
        //syncManager.SetRoomCustomPropertyData(PropertyKey.OrbPosList);
    }

    public void LeaveRoom()
    {
        Debug.Log("Master Client ����");
        PhotonNetwork.LeaveRoom();
    }

    #region PUN2 Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log("������ ���� ����");

        if (isConnecting)
            PhotonNetwork.JoinRandomRoom();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("���� ���� {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("���ο� ���� �����մϴ�.");

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayerPerRoom;

        PhotonNetwork.CreateRoom(null, roomOptions);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Space Room ����: Main Scene���� �̵�");
        PhotonNetwork.LoadLevel("SolarSystem_v5");
        isConnecting = false;
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("{0} ����", other.NickName);
        if (PhotonNetwork.IsMasterClient)
        {
            LoadSpace();
        }
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Space Room ����: Lobby Scene���� �̵�");
        //syncManager.SetRoomCustomPropertyData(PropertyKey.OrbData);
        //syncManager.SetRoomCustomPropertyData(PropertyKey.OrbPosList);
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("{0} ����", other.NickName);
        if (PhotonNetwork.IsMasterClient)
        {
            LoadSpace();
        }
    }

    // ������ Ŭ���̾�Ʈ �ٲ���� ��
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log($"������ �������ϴ�. ���� ������ {newMasterClient.NickName}�Դϴ�.");
        if (newMasterClient.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom != null)
            {
                //syncManager.SetCustomPropertiesFromMaster();
            }

            //GameManager_v4.Inst.UIManager.SetAccess_OrbdataEditor(newMasterClient.IsLocal);
        }
    }
    #endregion
}
