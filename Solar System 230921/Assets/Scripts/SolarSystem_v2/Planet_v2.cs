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
        // ���� ���� �ӵ��� ������ ������ ������
        if (orbitSpeed == 0)
            orbitSpeed = Random.Range(0.1f, 0.5f);

        // �¾�� �༺ ���� �Ÿ� 
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