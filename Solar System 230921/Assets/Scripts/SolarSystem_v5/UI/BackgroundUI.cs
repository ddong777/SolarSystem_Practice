using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundUI : AbstractUI
{
    [Header("NowOrb Data Panel")]
    public Text orbPosTxt;
    public Text camPosTxt;

    [Header("Exit")]
    public Button exitBtn;

    [Header("Etc")]
    public GameObject clientTxt;
    public Toggle camSyncTgl;

    public override void Init()
    {
        exitBtn.onClick.AddListener(GameManager_v4.Inst.NetworkManager.LeaveRoom);
        camSyncTgl.onValueChanged.AddListener((m_Toggle) => { ToggleCamSync(m_Toggle); });
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

    // �������� õü, ī�޶� ��ġ ���� ǥ��
    public void UpdateDataPanel(Vector3 orbPos, Vector3 camPos)
    {
        orbPosTxt.text = orbPos.ToString();
        camPosTxt.text = camPos.ToString();
    }

    public void ToggleCamSync(bool change)
    {
        if (camSyncTgl.isOn)
        {
            GameManager_v4.Inst.SyncManager.SetRoomCustomPropertyData(PropertyKey.OrbID);
        }
    }
}
