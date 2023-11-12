using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    public string startBtnName = "StartBtn";
    public string connectTxtName = "ConnectingTxt";

    private GameObject StartBtn;
    private GameObject progressTxt;

    private void Awake()
    {
        if (StartBtn == null) 
        { 
            StartBtn = GameObject.Find(startBtnName);
        }
        if (progressTxt == null)
        {
            progressTxt = GameObject.Find(connectTxtName);
        }

        SetStartBtnEvent();

        StartBtn.SetActive(true);
        progressTxt.SetActive(false);
    }

    private void SetStartBtnEvent()
    {
        EventManager_v5 eventManager = FindObjectOfType<EventManager_v5>();
        NetworkManager_v5 networkManager = FindObjectOfType<NetworkManager_v5>();
        eventManager.resetEvents();
        eventManager.SetEvent("enter", OnStartBtnPressed);
        eventManager.AddEvent("enter", networkManager.Connect);
    }

    public void OnStartBtnPressed()
    {
        StartBtn.SetActive(false);
        progressTxt.SetActive(true);
    }
}
