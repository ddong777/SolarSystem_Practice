using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BackgroundUI : MonoBehaviour
{
    private Transform camTrn;
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
        camTrn = FindObjectOfType<Camera>().transform;
    }

    public void Set(bool _access, Transform _orbTrn, UnityAction<bool> _syncFunc1)
    {
        SetAccess(_access);
        nowOrbTrn = _orbTrn;
        SetSyncToggle(_syncFunc1);
    }

    private void SetAccess(bool value)
    {
        clientTxt.gameObject.SetActive(value);
        camSyncTgl.gameObject.SetActive(value);
    }

    private void SetSyncToggle(UnityAction<bool> func)
    {
        camSyncTgl.onValueChanged.RemoveAllListeners();
        camSyncTgl.onValueChanged.AddListener((m_Toggle) => { func(m_Toggle); });
    }

    // bind로 관리하기
    private void SetExitBtn(UnityAction func)
    {
        exitBtn.onClick.RemoveAllListeners();
        exitBtn.onClick.AddListener(func);
    }

    public void UpdateUI()
    {
        orbPosTxt.text = nowOrbTrn.localPosition.ToString();
        camPosTxt.text = camTrn.position.ToString();
    }
}
