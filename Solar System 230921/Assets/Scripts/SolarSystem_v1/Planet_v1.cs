using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_v1 : Orb_v1
{
    [Header("공전")]
    public Transform centerTrn;
    public Transform planetTrn;
    public bool isOrbitClockwise = false;

    private float degree = 0f;
    private float distance;
    public float orbitSpeed;

    void Start()
    {
        if (planetTrn == null)
            planetTrn = transform.parent.transform;

        // 따로 공전 속도를 정하지 않으면 랜덤값
        if (orbitSpeed == 0)
            orbitSpeed = UnityEngine.Random.Range(0.1f, 0.5f);

        // 태양과 행성 사이 거리 
        distance = Vector3.Magnitude(planetTrn.position - centerTrn.position);
    }

    void Update()
    {
        Spin();
        Orbit();
    }

    void Orbit()
    {
        degree += orbitSpeed * Time.deltaTime;
        planetTrn.position = new Vector3(centerTrn.position.x + (distance * Mathf.Sin(degree)), 
                                         centerTrn.position.y,
                                         centerTrn.position.z + (distance * Mathf.Cos(degree)));
    }
}
