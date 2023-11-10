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

        StartBtn.GetComponent<Button>().onClick.AddListener(OnStartBtnPressed);

        StartBtn.SetActive(true);
        progressTxt.SetActive(false);
    }

    public void OnStartBtnPressed()
    {
        StartBtn.SetActive(false);
        progressTxt.SetActive(true);
    }
}
