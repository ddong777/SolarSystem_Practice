using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BackgroundUI : MonoBehaviour
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

    public void Init()
    {
        //camSyncTgl.onValueChanged.AddListener((m_Toggle) => { ToggleCamSync(m_Toggle); });
    }

    public void UpdateBackground()
    {

    }

    public void BackgroundChanged()
    {

    }

    public void SetExitBtn(UnityAction func)
    {
        exitBtn.onClick.RemoveAllListeners();
        exitBtn.onClick.AddListener(func);
    }

    public void SetAccess(bool value)
    {
        clientTxt.gameObject.SetActive(value);
        camSyncTgl.gameObject.SetActive(value);
    }
}
