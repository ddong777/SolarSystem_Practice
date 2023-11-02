using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemController : MonoBehaviour
{  
    public GameObject[] orbFeaturePrefab;

    [Serializable] 
    public struct orbData
    {
        [Rename("õü ����")]
        public OrbType orbType;

        [Space(10)]

        [Rename("�߽������� �Ÿ�")]
        public float orbPosX;
        [Rename("õü ����")]
        public float orbRotZ;
        [Rename("õü ũ��")]
        public float orbSize;

        [Space(10)]

        [Rename("���� ����")]
        public MoveDir spinDir;
        [Rename("���� �ӵ�")]
        public float spinSpeed;
        [Rename("���� ����")]
        public MoveDir orbitDir;
        [Rename("���� �ӵ�")]
        public float orbitSpeed;

        [Space(10)]

        [Rename("õü Transform")]
        public Transform orbTrn;
        [Rename("õü Axis")]
        public Transform orbAxis;
    }

    [Header("<---------�׼�--------->")]
    public orbData starData;

    [Header("<---------�༺--------->")]
    public Transform planetParentTrn;
    public GameObject planetSamplePrefab;
    public orbData[] planetData;

    private Orb_v2[] orbs;
 
    // �׼� ����
    void SetStar()
    {
        Star_v2 star = transform.GetComponentInChildren<Star_v2>();

        star.orbPos = new Vector3(starData.orbPosX, 0, 0);
        star.orbRot = new Vector3(0, 0, starData.orbRotZ);
        star.orbSize = starData.orbSize;

        star.orbPrefab = orbFeaturePrefab[(int)starData.orbType];
        star.OrbAxis = starData.orbAxis;

        star.SpinSpeed = starData.spinSpeed;
        star.SpinDir = starData.spinDir;
    }

    
    void SetPlanet()
    {
        Planet_v2[] planets = new Planet_v2[planetData.Length];

        // �༺ ����
        for (int i = 0; i < planetData.Length; i++)
        {
            GameObject newPlanet = Instantiate(planetSamplePrefab, planetParentTrn);
            planetData[i].orbTrn = newPlanet.transform;
            planetData[i].orbAxis = newPlanet.transform.GetChild(0);

            planets[i] = newPlanet.GetComponent<Planet_v2>();
        }

        // �༺ ����
        for (int i = 0; i < planets.Length; i++)
        {
            planets[i].orbPos = new Vector3(planetData[i].orbPosX, 0, 0);
            planets[i].orbRot = new Vector3(0, 0, planetData[i].orbRotZ);
            planets[i].orbSize = planetData[i].orbSize;

            planets[i].orbPrefab = orbFeaturePrefab[(int)planetData[i].orbType];
            planets[i].OrbAxis = planetData[i].orbAxis;
            planets[i].OrbTrn = planetData[i].orbTrn;
            planets[i].CenterTrn = starData.orbTrn;

            planets[i].SpinSpeed = planetData[i].spinSpeed;
            planets[i].SpinDir = planetData[i].spinDir;

            planets[i].OrbitSpeed = planetData[i].orbitSpeed;
            planets[i].OrbitDir = planetData[i].orbitDir;
        }
    }


    void Start()
    {
        SetStar();
        SetPlanet();

        // Orb_v2�� ����� ��� õü ������
        orbs = transform.GetComponentsInChildren<Orb_v2>();

        foreach (Orb_v2 orb in orbs)
        {
            orb.Init();
        }
    }

    void Update()
    {
        foreach (Orb_v2 orb in orbs)
        {
            orb.Move();
        }
    }
}
