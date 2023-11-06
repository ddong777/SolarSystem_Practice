using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIData_v5 : DataSender
{
    private Dictionary<string, float> nowOrbData = new Dictionary<string, float>()
    {
        { "id", 0f },
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
                nowOrbID = (int)nowOrbData["id"];
                SendData();
            }
        }
    }

    private int nowOrbID;
    public int NowOrbID
    {
        get => nowOrbID;
        set
        {
            if (nowOrbID == value)
            {
                nowOrbID = value;
            }
        }
    } 
    
    private string[] orbNameList;
    public string[] SelectorUIData_OrbNameList
    {
        get=> orbNameList;
        set
        {
            if (orbNameList == value)
            {
                orbNameList = value;
                SendData();
            }
        }
    }

    private bool isAcessible = true;
    public bool IsAccessible
    {
        get => isAcessible;
        set
        {
            if (isAcessible == value)
            {
                isAcessible = value;
                SendData();
            }
        }
    }

    private void OnEnable()
    {
        SendData();
    }
}