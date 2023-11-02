using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_v3 : Orb_v3
{
    public Transform childenTrn;

    private void Awake()
    {
        childenTrn = transform.GetChild(0).Find("Planets");
    }

    public override void InitTransform()
    {
        orbTrn.localPosition = Vector3.zero;
        orbTrn.localRotation = Quaternion.Euler(new Vector3(0, 0, orbData.orbRotZ));
    }

    public override void Init()
    {
        base.Init();
        InitSpin();
    }

    public override void Move()
    {
        Spin();
    }
}
