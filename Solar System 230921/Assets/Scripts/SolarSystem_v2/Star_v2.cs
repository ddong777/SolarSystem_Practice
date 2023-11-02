using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_v2 : Orb_v2, ISpinable
{
    // Spin용 변수
    [SerializeField] private Transform orbAxis;
    [SerializeField] private float spinSpeed = 10f;
    [SerializeField] private MoveDir spinDir;

    public Transform OrbAxis 
    { 
        get => orbAxis; 
        set => orbAxis = value; 
    }
    public float SpinSpeed
    {
        get => spinSpeed;
        set => spinSpeed = value;
    }
    public MoveDir SpinDir
    {
        get => spinDir;
        set => spinDir = value;
    }

    public override void Init()
    {
        base.Init();
        InitSpin();
        //Debug.Log("init star");
    }

    public override void Move()
    {
        base.Move();
        Spin();
    }

    public void InitSpin()
    {
        if (orbAxis == null)
            OrbAxis = transform.GetChild(0);

        // 따로 자전 속도를 정하지 않으면 랜덤값
        if (spinSpeed == 0)
            spinSpeed = Random.Range(10f, 100f);
    }

    public void Spin()
    {
        if (spinDir == MoveDir.rightDir)
            orbAxis.Rotate(spinSpeed * Time.deltaTime * Vector3.up);
        else
            orbAxis.Rotate(-spinSpeed * Time.deltaTime * Vector3.up);
    }
}
