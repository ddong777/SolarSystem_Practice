using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_v3 : Orb_v3
{
    public override void InitTransform()
    {
        float star_r = centerTrn.localScale.x * 0.5f;
        orbTrn.localPosition = new Vector3(orbData.orbPosX + star_r, 0, 0);
        orbTrn.localRotation = Quaternion.Euler(new Vector3(0, 0, orbData.orbRotZ));
    }

    public override void Init()
    {
        base.Init();

        InitSpin();
        InitOrbit();
    }

    public override void Move()
    {
        Spin();
        Orbit();
    }
}
