using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    public string startBtnName = "StartBtn";
    public string connectTxtName = "ConnectingTxt";

    private GameObject StartBtn;
    private GameObject progressText;

    private void Awake()
    {
        if (StartBtn == null) 
        { 
            StartBtn = GameObject.Find(startBtnName);
        }
        if (progressText == null)
        {
            progressText = GameObject.Find(connectTxtName);
        }

        StartBtn.GetComponent<Button>().onClick.AddListener(Connect);

        StartBtn.SetActive(true);
        progressText.SetActive(false);
    }

    public void Connect()
    {
        StartBtn.SetActive(false);
        progressText.SetActive(true);
    }
}
