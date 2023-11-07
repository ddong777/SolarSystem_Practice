using ExitGames.Client.Photon.StructWrapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContainer : MonoBehaviour
{
    private bool isMaster;
    public bool IsMaster
    {
        get { return isMaster; }
        set { isMaster = value; }
    }

    private bool isSyncMode;
    public bool IsSyncMode
    {
        get { return isSyncMode; }
        set { isSyncMode = value; }
    }

    private int nowOrbID;
    public int NowOrbID
    {
        get { return nowOrbID; }
        set { nowOrbID = value; }
    }

    private List<OrbData> orbDatas = new List<OrbData>();
    public List<OrbData> OrbDatas
    {
        get 
        {
            return orbDatas; 
        }
        set 
        { 
            if (orbDatas != value)
            {
                orbDatas = value;
            }
        }
    }

    private List<Transform> orbTrns = new List<Transform>();
    public List<Transform> OrbTrns
    {
        get 
        { 
            return orbTrns; 
        }
        set 
        {
            if (orbTrns != value)
            {
                orbTrns = value;
            }
        }
    }
}
