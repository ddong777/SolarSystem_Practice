using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager_v5 : MonoBehaviour
{
    private Data_UI data;

    public EditorUI dataEditUI;
    public SelectorUI orbSelectUI;
    public BackgroundUI singleUI;

    private bool isReady = false;

    private void Update()
    {
        if (!isReady)
        {
            return;
        }
        singleUI.UpdateUI();
    }

    public void Init()
    {
        isReady = false;

        data = FindObjectOfType<Data_UI>();
        data.Init();

        dataEditUI = GetComponentInChildren<EditorUI>();
        orbSelectUI = GetComponentInChildren<SelectorUI>();
        singleUI = GetComponentInChildren<BackgroundUI>();

        dataEditUI.Init();
        orbSelectUI.Init();
        singleUI.Init();

        data.Attach(dataEditUI);
        data.Attach(orbSelectUI);
        data.Attach(singleUI);

        dataEditUI.Attach(data);
        orbSelectUI.Attach(data);
        singleUI.Attach(data);
    }

    public void Set()
    {
        dataEditUI.Set(data.IsMaster, data.NowOrbData);
        orbSelectUI.Set(data.OrbDatas);
        singleUI.Set(data.IsMaster, data.NowOrbTrn);

        isReady = true;
    }
}
