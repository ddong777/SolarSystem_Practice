using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

public class NetworkManager_v5 : MonoBehaviourPunCallbacks
{
    private static NetworkManager_v5 instance;
    private SyncManager_v5 syncManager;

    private string gameVersion = "2";
    private string playerNamePrefKey = "player__";
    private bool isConnecting;

    [SerializeField]
    private byte maxPlayerPerRoom = 4;

    private static bool isInitialized = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }
        else
        {
            Destroy(this.gameObject);
        }

        Init();

        // �ѹ��� �ʱ�ȭ
        if (!isInitialized) 
        {
            Set();
        }
    }

    public void Init()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            SetPlayerName();
        } else
        {
            if (syncManager == null)
            {
                syncManager = FindObjectOfType<SyncManager_v5>();
            }
        }
    }

    public void Set()
    {
        EventManager_v5 eventManager = FindObjectOfType<EventManager_v5>();
        eventManager.AddBaseEvent("enter", Connect);
        eventManager.AddBaseEvent("exit", LeaveRoom);
        isInitialized = true;
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

    /// <summary>
    /// �κ񿡼� �����ϴ� �Լ���
    /// </summary>
    #region Launcher Methods
    public void Connect()
    {
        //Debug.Log("Connect");
        isConnecting = true;

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("������ ���� ����");

        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
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
    #endregion

    
    /// <summary>
    /// ���� ������ �����ϴ� �Լ���
    /// </summary>
    #region main Scene
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
        syncManager.Send_FromMaster();
    }

    public void LeaveRoom()
    {
        Debug.Log("Master Client ����");
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Space Room ����: Lobby Scene���� �̵�");

        syncManager.Send_FromMaster();
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("{0} ����", other.NickName);
        if (PhotonNetwork.IsMasterClient)
        {
            LoadSpace();
        }
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

            syncManager.OnMasterClientChange(); 
        }
    }
    #endregion
}
