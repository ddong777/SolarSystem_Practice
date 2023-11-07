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
    }
}
