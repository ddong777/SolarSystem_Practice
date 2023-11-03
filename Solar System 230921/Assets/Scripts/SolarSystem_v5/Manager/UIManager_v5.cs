using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_v5 : _Manager_v5
{
    public List<AbstractUI> UIs = new List<AbstractUI>();
    private OrbDataEditorUI dataEditUI;
    private OrbSelectorUI orbSelectUI;
    private BackgroundUI singleUI;

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
    public void Set_OrbSelector(string[] orbList)
    {
        orbSelectUI.orbNameData = orbList;
    }
    public void Set_SingleUIs(Transform orbTrn)
    {
        singleUI.nowOrbTrn = orbTrn;
    }
}
