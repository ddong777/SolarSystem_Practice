using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_v4 : Planet_v3
{
    public override void Orbit()
    {
        if (centerTrn == null)
        {
            if (GameManager_v4.Inst.SolarSystem.star == null) return;
            centerTrn = GameManager_v4.Inst.SolarSystem.star.transform;
        }

        Vector3 newPos;
        Vector3 dir = centerTrn.position - orbTrn.localPosition;
        Vector3 movedir;
        float subDis = dir.magnitude - Distance;

        if (orbData.orbitDir == MoveDir.leftDir)
        {
            movedir = Vector3.Cross(Vector3.up, dir).normalized * (OrbitSpeed * 100f) * Time.deltaTime;

            newPos = orbTrn.localPosition;
            newPos += movedir;
            newPos += dir.normalized * subDis;
        }
        else
        {
            movedir = Vector3.Cross(dir, Vector3.up).normalized * (OrbitSpeed * 100f) * Time.deltaTime;

            newPos = orbTrn.localPosition;
            newPos += movedir;
            newPos += dir.normalized * subDis;
        }

        orbTrn.localPosition = newPos;
    }
}
