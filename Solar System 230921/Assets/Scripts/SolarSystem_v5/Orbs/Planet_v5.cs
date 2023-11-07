using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SolarSystemController;

public class Planet_v5 : Orb_v5
{
#if UNITY_EDITOR
    [Rename("공전 중심점")]
#endif
    public Transform centerTrn;
    private float distance;

    public override void InitMove()
    {
        InitSpin();
        InitOrbit();
    }

    public override void Move()
    {
        Spin();
        Orbit();
    }

    public override void InitOrbit()
    {
        base.InitOrbit();
        distance = data.orbPosX;
    }

    public override void Orbit()
    {
        Vector3 newPos;
        Vector3 dir = centerTrn.position - orbitTrn.localPosition;
        Vector3 movedir;
        float subDis = dir.magnitude - distance;

        if (data.orbitDir == MoveDir.leftDir)
        {
            movedir = Vector3.Cross(Vector3.up, dir).normalized * (data.orbitSpeed * 100f) * Time.deltaTime;

            newPos = orbitTrn.localPosition;
            newPos += movedir;
            newPos += dir.normalized * subDis;
        }
        else
        {
            movedir = Vector3.Cross(dir, Vector3.up).normalized * (data.orbitSpeed * 100f) * Time.deltaTime;

            newPos = orbitTrn.localPosition;
            newPos += movedir;
            newPos += dir.normalized * subDis;
        }

        orbitTrn.localPosition = newPos;
    }
}
