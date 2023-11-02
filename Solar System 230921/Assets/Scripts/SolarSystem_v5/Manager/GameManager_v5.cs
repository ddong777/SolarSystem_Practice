using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_v5 : _Manager_v5
{
    bool isMaster = true;
    
    private static GameManager_v5 instance;

    private OrbDataManager_v5 orbDataManager;
    private OrbFactory orbFactory;
    private SolarSystemController_v5 solarSystem;

    private UIDataManager_v5 uiDataManager;
    private UIManager_v5 uiManager;


    /// <summary>
    /// ���� �Լ���
    /// </summary>
    private void Awake()
    {
        Screen.SetResolution(960, 540, false);

        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(this.gameObject);
        }
    }

    private void OnEnable()
    {
        Init();
        Set();
    }

    private void Update()
    {
        solarSystem.MoveAllOrbs();
    }

    /// <summary>
    /// ���� ����
    /// 1. �� ����
    /// 2. ����ڿ����� ������ ����(�� �籸��)
    ///     1) ������ UI�� ���� orb ������ ����
    ///         - ������ UI ������Ʈ
    ///         - ������ UI ��� ������Ʈ
    ///         - Orb Data �Ŵ��� ������Ʈ
    ///         - SolarSystem�� �ش��ϴ� Orb Update
    ///         - ���������� ������Ʈ
    /// 3. ������ ���� ������ ����(�� �籸��)
    /// </summary>
    private void Init()
    {
        orbDataManager = FindObjectOfType<OrbDataManager_v5>();
        solarSystem = FindObjectOfType<SolarSystemController_v5>();
    }
    private void Set()
    {
        SetDataManager();
        SetSolarSystem();
    }
    private void OrbChanged()
    {

    }
    private void OrbSelected()
    {

    }
    
    /// <summary>
    /// ������ �ִ� ��ü�� ��� �з�
    /// </summary>
    private void SetDataManager()
    {
        orbDataManager.Attach(this);
        orbDataManager.Attach(solarSystem);

        if (isMaster)
        {
            orbDataManager.Set();
        }
        else
        {
            //orbDataManager.Set(
            //    customPropertiesDataManager.GetCustomProperty_stringArray(PropertyKey.OrbData));
        }
    }

    private void SetSolarSystem()
    {
        foreach (OrbData data in orbDataManager.orbDatas)
        {
            if (data.isCenterOrb)
            {
                orbFactory = new StarFactory();
                orbFactory.Set(solarSystem.transform);

                solarSystem.SetStar(orbFactory.Create(data) as Star_v5);
            }
            else
            {
                orbFactory = new PlanetFactory();
                orbFactory.Set(solarSystem.star.childenTrn);

                solarSystem.SetOrb(orbFactory.Create(data));
            }
        }
    }

    private void UpdateSolarSystem()
    {

    }

    private void SetUIManager()
    {

    }

    private void UpdateUiManager()
    {

    }

    /// <summary>
    /// ������
    /// </summary>
    public override void OnNotify()
    {
        
    }
}
