using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Cam : AData
{
    public Transform TargetTrn => data.OrbTrns[data.NowOrbID];
    public float TargetSize => data.OrbDatas[data.NowOrbID].orbSize;
}
