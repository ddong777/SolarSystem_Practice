using System.Collections.Generic;
using UnityEngine;

public class SolarSystemController_v4 : MonoBehaviour
{
    public GameObject starTemplete;
    public GameObject planetTemplete;

    private OrbDataManager_v4 dataManager;
    private static SolarSystemController_v4 instance;

    [Space(10)]

    public Star_v3 star;
    public List<Orb_v3> orbs;

    public Vector3[] orbsPos;
    public Vector3[] orbsRot;

    public void Init()
    {
        // 한 씬에 마스터 클라이언트가 생성한 태양계 이외에 생성 X
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        dataManager = GameManager_v4.Inst.DataManager;

        if (starTemplete == null)
            starTemplete = Resources.Load<GameObject>($"Prefabs/Templete/V4/StarTrn_v4");

        if (planetTemplete == null)
            planetTemplete = Resources.Load<GameObject>($"Prefabs/Templete/V4/PlanetTrn_v4");
    }

    public void CreateOrb(OrbData data)
    {
        GameObject newOrb;

        if (data.isCenterOrb)
        {
            newOrb = Instantiate(starTemplete, transform);
            star = newOrb.GetComponent<Star_v3>();
        }
        else
        {
            newOrb = Instantiate(planetTemplete, star.childenTrn);
        }

        Orb_v3 orbD = newOrb.GetComponent<Orb_v3>();
        orbs.Add(orbD);
        dataManager.SetOrb(orbD.orbData, data.id);
    }

    public void EditOrb(int index)
    {
        dataManager.SetOrb(orbs[index].orbData, index);
        orbs[index].SetFeature();
    }

    public void CreatOrbs()
    {
        foreach (OrbData data in dataManager.orbDatas)
        {
            CreateOrb(data);
        }
    }

    public void SetOrbs()
    {
        foreach (Orb_v3 orb in orbs)
        {
            orb.centerTrn = star.transform;
            orb.Init();
            dataManager.UpdateData(orb.orbData, orb.orbData.id);
        }
    }

    public void UpdateDatas()
    {
        for (int i = 0; i < orbs.Count; i++)
        {
            dataManager.UpdateData(orbs[i].orbData, i);
        }
    }

    public void MoveOrbs()
    {
        foreach (Orb_v3 orb in orbs)
            orb.Move();
    }
        
    public Vector3[] GetOrbsPos()
    {
        orbsPos = new Vector3[orbs.Count];

        for (int i = 0; i < orbs.Count; i++)
        {
            orbsPos[i] = orbs[i].transform.localPosition;
        }

        return orbsPos;
    }

    public Vector3[] GetOrbsRot()
    {
        orbsRot = new Vector3[orbs.Count];

        for (int i = 0; i < orbs.Count; i++)
        {
            orbsRot[i] = orbs[i].orbAxis.localRotation.eulerAngles;
        }

        return orbsRot;
    }

    public void UpdateOrbsTrnFromServer(Vector3[] posArr, Vector3[] rotArr)
    {
        orbsPos = posArr;
        orbsRot = rotArr;

        for (int i = 0; i < orbs.Count; i++)
        {
            orbs[i].transform.localPosition = orbsPos[i]; 
            orbs[i].orbAxis.localRotation = Quaternion.Euler(orbsRot[i]);
        }
    }
}
