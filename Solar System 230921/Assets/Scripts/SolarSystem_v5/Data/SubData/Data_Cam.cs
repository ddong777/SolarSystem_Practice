using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Cam : AData
{
    public Transform TargetTrn
    {
        get
        {
            return data.OrbTrns[data.NowOrbID];
        }
    }
}
