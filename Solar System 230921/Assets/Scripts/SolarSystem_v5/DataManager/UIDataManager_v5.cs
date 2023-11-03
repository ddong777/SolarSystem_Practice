using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDataManager_v5 : _DataManager_v5
{
    public int NowOrbID;

    public Dictionary<string, float> nowOrbData = new Dictionary<string, float>()
    {
        { "orbType", 0f },
        { "orbPosX", 0f },
        { "orbRotZ", 0f },
        { "orbSize", 0f },
        { "spinDir", 0f },
        { "spinSpeed", 0f },
        { "orbitDir", 0f },
        { "orbitSpeed", 0f }
    };
    public Dictionary<string, float> EditorUIData_NowOrbData
    {
        get => nowOrbData;
        set 
        {
            if (value != nowOrbData)
            {
                nowOrbData = value;
                Notify();
            }
        }
    }

    public string[] SelectorUIData_OrbNameList;
    public Transform SingleUIData_NowOrbTrn;

    public override void Notify()
    {
        foreach (_Manager_v5 manager in observers)
        {
            manager.OnNotify();
        }
    }
}