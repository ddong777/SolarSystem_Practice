using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Orb_v3 : MonoBehaviour, ISpinable, IOrbitable
{
    public OrbData orbData;

    [Space(10)]
#if UNITY_EDITOR
    [Rename("자전축")]
#endif
    public Transform orbAxis;
#if UNITY_EDITOR
    [Rename("공전 주체")]
#endif
    public Transform orbTrn;
#if UNITY_EDITOR
    [Rename("공전 중심점")]
#endif
    public Transform centerTrn;

    [Space(10)]
    public Transform orbFeatureTrn;

    private float distance;
    private float degree;

    public float SpinSpeed 
    { 
        get => orbData.spinSpeed;
        set => orbData.spinSpeed = value;
    }
    public MoveDir SpinDir
    {
        get => orbData.spinDir;
        set => orbData.spinDir = value;
    }
    public float OrbitSpeed
    {
        get => orbData.orbitSpeed;
        set => orbData.orbitSpeed = value;  
    }
    public MoveDir OrbitDir
    {
        get => orbData.orbitDir;
        set => orbData.orbitDir = value;
    }

    public float Distance => distance;

    public virtual void Init()
    {
        if (orbTrn == null) orbTrn = transform;
        if (orbAxis == null) orbAxis = orbTrn.GetChild(0);
        if (orbFeatureTrn == null) orbFeatureTrn = orbAxis.Find("Feature");
        
        InitTransform();
        SetFeature();
    }

    public virtual void InitTransform() { }

    public void SetFeature()
    {
        // 가지고 있는 prefab 있을 경우날리고 다시 생성
        for (int i = 0; i < orbFeatureTrn.childCount; i++)
            Destroy(orbFeatureTrn.GetChild(i).gameObject);

        Instantiate(orbData.orbPrefab, orbFeatureTrn);
        orbFeatureTrn.localScale = new Vector3(orbData.orbSize, orbData.orbSize, orbData.orbSize);
        distance = orbData.orbPosX;
    }

    public void InitSpin()
    {
        // 따로 자전 속도를 정하지 않으면 랜덤값
        SpinSpeed = orbData.spinSpeed;
        if (orbData.spinSpeed == 0)
        {
            SpinSpeed = Random.Range(10f, 100f);
        }            
    }

    public void Spin()
    {
        if (orbAxis == null) return;

        if (orbData.spinDir == MoveDir.rightDir)
            orbAxis.Rotate(SpinSpeed * Time.deltaTime * Vector3.up);
        else
            orbAxis.Rotate(-SpinSpeed * Time.deltaTime * Vector3.up);
    }

    public void SetCenterTrn(Transform _center)
    {
        centerTrn = _center;
    }

    public virtual void InitOrbit()
    {
        // 따로 공전 속도를 정하지 않으면 랜덤값
        if (orbData.orbitSpeed == 0)
        {
            OrbitSpeed = Random.Range(0.2f, 1f);
        }

        // 태양과 행성 사이 거리 
        distance = Vector3.Magnitude(orbTrn.position - centerTrn.position);
    }

    public virtual void Orbit()
    {
        if (centerTrn == null) return;

        degree += OrbitSpeed * Time.deltaTime;
        Vector3 newPos;

        if (orbData.orbitDir == MoveDir.leftDir)
        {
            newPos = new Vector3(centerTrn.position.x + (distance * Mathf.Sin(degree)),
                                 centerTrn.position.y,
                                 centerTrn.position.z + (distance * Mathf.Cos(degree)));
        }
        else
        {
            newPos = new Vector3(centerTrn.position.x + (distance * Mathf.Cos(degree)),
                                 centerTrn.position.y,
                                 centerTrn.position.z + (distance * Mathf.Sin(degree)));
        }

        orbTrn.localPosition = newPos;
    }

    public virtual void Move() { }
}
