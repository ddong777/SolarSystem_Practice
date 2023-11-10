using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Cam : EssentialData
{
    public override bool IsSyncMode => data.IsSyncMode;
    public Transform TargetTrn => data.OrbTrns[data.NowOrbID];
    public float TargetSize => data.OrbDatas[data.NowOrbID].orbSize;
}
