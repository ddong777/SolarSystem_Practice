using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BackgroundUI : MonoBehaviour,ISender, IReceiver
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

    public void Set(bool _access, Transform _orbTrn)
    {
        SetAccess(_access);
        SetNowOrbTrn(_orbTrn);
        SetSyncToggle();
    }

    private void SetAccess(bool value)
    {
        clientTxt.gameObject.SetActive(value);
    }

    private void SetNowOrbTrn(Transform _orbTrn)
    {
        nowOrbTrn = _orbTrn;
    }

    private void SetSyncToggle()
    {
        camSyncTgl.onValueChanged.RemoveAllListeners();
        camSyncTgl.onValueChanged.AddListener((m_Toggle) => { SendData(m_Toggle); });
    }

    public void UpdateUI()
    {
        orbPosTxt.text = nowOrbTrn.localPosition.ToString();
        camPosTxt.text = camTrn.position.ToString();
    }

    //=======================================================================
    private List<IReceiver> receivers = new List<IReceiver>();

    public void Attach(IReceiver receiver)
    {
        receivers.Add(receiver);
    }

    public void Detach(IReceiver receiver)
    {
        receivers.Remove(receiver);
    }

    public void SendData<T>(T data)
    {
        foreach (IReceiver r in receivers)
        {
            r.ReceiveData(data);
        }
    }
    public void ReceiveData<T>(T _data)
    {
        if (_data is bool)
        {
            // isMaster
            SetAccess((bool)(object)_data);
        } 
        else if (_data is Transform)
        {
            SetNowOrbTrn(_data as Transform);
        }
    }
}
