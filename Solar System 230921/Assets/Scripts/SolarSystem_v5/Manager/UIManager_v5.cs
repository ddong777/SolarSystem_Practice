using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager_v5 : MonoBehaviour
{
    private Data_UI data;

    public OrbDataEditorUI dataEditUI;
    public OrbSelectorUI orbSelectUI;
    public BackgroundUI singleUI;

    private void Update()
    {
        singleUI.UpdateUI();
    }

    public void Init()
    {
        data = FindObjectOfType<Data_UI>();
        data.Init();

        dataEditUI = GetComponentInChildren<OrbDataEditorUI>();
        orbSelectUI = GetComponentInChildren<OrbSelectorUI>();
        singleUI = GetComponentInChildren<BackgroundUI>();

        dataEditUI.Init();
        orbSelectUI.Init();
        singleUI.Init();

        data.Attach(dataEditUI);
        data.Attach(orbSelectUI);
        data.Attach(singleUI);
    }

    public void Set()
    {
        dataEditUI.Set(data.IsMaster, data.NowOrbData);
        orbSelectUI.Set(data.OrbDatas);
        singleUI.Set(data.IsMaster, data.NowOrbTrn, (value) => { data.IsSyncMode = value; });
    }
}
