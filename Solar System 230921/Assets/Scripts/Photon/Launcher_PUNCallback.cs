using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class Launcher_PUNCallback : MonoBehaviourPunCallbacks
{
    string gameVersion = "1";
    string playerNamePrefKey = "player";
    bool isConnecting;

    public GameObject controlPanel;
    public GameObject progressPanel;

    [Space(10)]
    [SerializeField] 
    private byte maxPlayerPerRoom = 4;

    #region MonoPunCallback Callback
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        controlPanel.SetActive(true);
        progressPanel.SetActive(false);

        if (PlayerPrefs.HasKey(playerNamePrefKey))
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString(playerNamePrefKey);
        }
        else
        {
            string name = $"player_{PhotonNetwork.AuthValues.UserId}";
            PhotonNetwork.NickName = name;
            PlayerPrefs.SetString(playerNamePrefKey, name);
        }
    }


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
        PhotonNetwork.LoadLevel("SolarSystem_v4");
    }
    #endregion

    public void Connect()
    {
        isConnecting = true;

        controlPanel.SetActive(false);
        progressPanel.SetActive(true);

        if (PhotonNetwork.IsConnected)
            PhotonNetwork.JoinRandomRoom();
        else
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
}
