using Photon.Pun;
using Photon.Realtime;

using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkTest : MonoBehaviourPunCallbacks
{
    private string gameVersion = "3";
    private string playerNamePrefKey = "player";
    private bool isConnecting;

    [SerializeField]
    private byte maxPlayerPerRoom = 4;

    private static bool isInitialized = false;

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

        // Debug.Log(isInitialized);
        if (!isInitialized)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            SetPlayerName();
            Set();
        }
    }

    public void Set()
    {
        Debug.Log("network set");
        EventManager_v5 eventManager = FindObjectOfType<EventManager_v5>();
        eventManager.AddEvent("enter", Connect);
        eventManager.AddEvent("exit", LeaveRoom);
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

    }

    public void Connect()
    {
        Debug.Log("connect");
        isConnecting = true;

        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
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
        PhotonNetwork.LoadLevel("TestScene1");
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
            }

        }
    }
    #endregion
}