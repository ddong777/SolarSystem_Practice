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

    // 필요 데이터
    // OrbData
    // List<OrbData.orbType>
    // Orb Pos/Rot
    // OrbTrn
    // isMaster

    public void Init()
    {
        dataEditUI = GetComponentInChildren<OrbDataEditorUI>();
        orbSelectUI = GetComponentInChildren<OrbSelectorUI>();
        singleUI = GetComponentInChildren<BackgroundUI>();

        UIs.Add(dataEditUI);
        UIs.Add(orbSelectUI);
        UIs.Add(singleUI);

        foreach (AbstractUI ui in UIs)
        {
            ui.Init();
        }
    }

    public void Set_OrbDataEditor()
    {

    }
    public void Set_OrbSelector()
    {

    }
    public void Set_SingleUIs()
    {

    }

    public void Update_OrbDataEditor()
    {            
                 
    }
    public void Update_OrbSelector()
    {            
                 
    }
    public void Update_SingleUIs()
    {

    }


    public override void OnNotify()
    {
        foreach(AbstractUI ui in UIs)
        {
            ui.OnUpdateData();
        }
    }
}
