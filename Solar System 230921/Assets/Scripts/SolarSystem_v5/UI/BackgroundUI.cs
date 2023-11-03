using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundUI : AbstractUI
{
    public Transform nowOrbTrn;

    [Header("NowOrb Data Panel")]
    public Text orbPosTxt;
    public Text camPosTxt;

    [Header("Exit")]
    public Button exitBtn;

    [Header("Master Client")]
    public GameObject clientTxt;

    [Header("Cam Sync Toggle")]
    public Toggle camSyncTgl;

    public override void Init()
    {
        //exitBtn.onClick.AddListener(GameManager_v4.Inst.NetworkManager.LeaveRoom);
        //camSyncTgl.onValueChanged.AddListener((m_Toggle) => { ToggleCamSync(m_Toggle); });
    }

    public void UpdateBackground()
    {

    }

    public void BackgroundChanged()
    {

    }

    public void SetAccess(bool value)
    {
        clientTxt.gameObject.SetActive(value);
        camSyncTgl.gameObject.SetActive(value);
    }
}
