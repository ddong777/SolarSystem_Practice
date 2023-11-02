using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Star_v5 : Orb_v5
{
    public Transform childenTrn;
    
    public override void InitMove()
    {
        InitSpin();
    }
    
    public override void Move()
    {
        Spin();
    }
}
