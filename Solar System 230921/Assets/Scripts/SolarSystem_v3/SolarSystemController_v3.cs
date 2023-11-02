using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemController_v3 : MonoBehaviour
{
    public GameObject starTemplete;
    public GameObject planetTemplete;

    private static SolarSystemController_v3 instance;

    private OrbDataManager_v3 dataManager;

    [Space(10)]

    public Star_v3 star;
    public List<Orb_v3> orbs;

    public void Init()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        dataManager = FindObjectOfType<OrbDataManager_v3>();
    }

    public void CreateOrb(OrbData data)
    {
        GameObject newOrb;

        if (data.isCenterOrb)
        {
            newOrb = Instantiate(starTemplete, transform);
            star = newOrb.GetComponent<Star_v3>();
            
        } else
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
        orbs[index].Init();
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
            if (!orb.orbData.isCenterOrb) orb.centerTrn = star.transform;
            orb.Init();
            dataManager.UpdateData(orb.orbData, orb.orbData.id);
        }
    }

    private void Awake()
    {
        Screen.SetResolution(960, 540, false);

        Init();
        dataManager.Init();
        
        CreatOrbs();
    }

    private void Start()
    {
        SetOrbs();
    }

    private void Update()
    {
        MoveOrbs();
    }

    public void MoveOrbs()
    {
        foreach (Orb_v3 orb in orbs)
            orb.Move();
    }

    public void DestroyOrb(int index)
    {
        orbs.RemoveAt(index);
        dataManager.RemoveData(index);
        Destroy(orbs[index]);
    }

    public void UpdateDatas()
    {
        for (int i = 0; i < orbs.Count; i++)
        {
            dataManager.UpdateData(orbs[i].orbData, i);
        }
    }
}
