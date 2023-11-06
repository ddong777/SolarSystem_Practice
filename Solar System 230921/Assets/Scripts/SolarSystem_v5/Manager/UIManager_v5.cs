using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager_v5 : MonoBehaviour
{
    public OrbDataEditorUI dataEditUI;
    public OrbSelectorUI orbSelectUI;
    public BackgroundUI singleUI;

    public void Init()
    {
        dataEditUI = GetComponentInChildren<OrbDataEditorUI>();
        orbSelectUI = GetComponentInChildren<OrbSelectorUI>();
        singleUI = GetComponentInChildren<BackgroundUI>();

        dataEditUI.Init();
        orbSelectUI.Init();
        singleUI.Init();
    }

    public void SetAccess(bool isAccessible)
    {
        dataEditUI.SetAccess(isAccessible);
        singleUI.SetAccess(isAccessible);
    }

    public void Set_OrbDataEditor(Dictionary<string,float> orbData)
    {
        dataEditUI.SetEditor(orbData);
    }
    public void Set_OrbSelector(Dictionary<int, Dictionary<string, float>> datas)
    {
        orbSelectUI.SetSelector(datas);
    }
    public void Set_SingleUIs(Transform orbTrn, UnityAction func)
    {
        singleUI.nowOrbTrn = orbTrn;
        singleUI.SetExitBtn(func);
    }
}
