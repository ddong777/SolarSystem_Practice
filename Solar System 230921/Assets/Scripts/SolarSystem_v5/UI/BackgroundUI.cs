using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BackgroundUI : MonoBehaviour, IReceiver
{
    private Transform camTrn;
    public Transform nowOrbTrn;

    [Header("NowOrb Data Panel")]
    public Text orbPosTxt;
    public Text camPosTxt;

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
        SetNowOrbTrn(_orbTrn);
        SetSyncToggle(_syncFunc1);
    }

    private void SetAccess(bool value)
    {
        clientTxt.gameObject.SetActive(value);
        camSyncTgl.gameObject.SetActive(value);
    }

    private void SetNowOrbTrn(Transform _orbTrn)
    {
        nowOrbTrn = _orbTrn;
    }

    private void SetSyncToggle(UnityAction<bool> func)
    {
        camSyncTgl.onValueChanged.RemoveAllListeners();
        camSyncTgl.onValueChanged.AddListener((m_Toggle) => { func(m_Toggle); });
    }

    public void UpdateUI()
    {
        orbPosTxt.text = nowOrbTrn.localPosition.ToString();
        camPosTxt.text = camTrn.position.ToString();
    }

    //=======================================================================

    public void ReceiveData<T>(T _data)
    {
        if (_data.GetType() == typeof(bool))
        {
            SetAccess((bool)(object)_data);
        } 
        else if (_data.GetType() == typeof(Transform))
        {
            SetNowOrbTrn(_data as Transform);
        }
    }
}
