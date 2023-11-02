using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_v2 : Star_v2, IOrbitable
{
    [SerializeField] private Transform orbTrn;
    [SerializeField] private Transform centerTrn;

    [SerializeField] private float orbitSpeed;
    [SerializeField] private MoveDir orbitDir;

    private float degree;
    private float distance;

    public Transform OrbTrn
    {
        get => orbTrn;
        set => orbTrn = value;
    }
    public Transform CenterTrn
    {
        get => centerTrn;
        set => centerTrn = value;
    }
    public float OrbitSpeed
    {
        get => orbitSpeed;
        set => orbitSpeed = value;
    }
    public MoveDir OrbitDir
    {
        get => orbitDir;
        set => orbitDir = value;
    }

    public float Degree => degree;

    public float Distance => distance;

    public override void Init()
    {
        base.Init();
        InitOrbit();
        //Debug.Log("init planet");
    }

    public override void Move()
    {
        base.Move();
        Orbit();
    }

    public void InitOrbit()
    {
        // 따로 공전 속도를 정하지 않으면 랜덤값
        if (orbitSpeed == 0)
            orbitSpeed = Random.Range(0.1f, 0.5f);

        // 태양과 행성 사이 거리 
        distance = Vector3.Magnitude(orbTrn.position - centerTrn.position);
    }

    public void Orbit()
    {
        degree += orbitSpeed * Time.deltaTime;

        if (orbitDir == MoveDir.leftDir)
        {
            orbTrn.position = new Vector3(centerTrn.position.x + (distance * Mathf.Sin(degree)),
                                          centerTrn.position.y,
                                          centerTrn.position.z + (distance * Mathf.Cos(degree)));
        } else
        {
            orbTrn.position = new Vector3(centerTrn.position.x + (distance * Mathf.Cos(degree)),
                                          centerTrn.position.y,
                                          centerTrn.position.z + (distance * Mathf.Sin(degree)));
        }
    }
}