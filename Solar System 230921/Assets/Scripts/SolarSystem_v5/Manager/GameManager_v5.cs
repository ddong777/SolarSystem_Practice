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
    /// 내장 함수들
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
    /// 게임 상태
    /// 1. 씬 구성
    /// 2. 사용자에의한 데이터 변경(씬 재구성)
    ///     1) 에디터 UI를 통한 orb 데이터 변경
    ///         - 에디터 UI 업데이트
    ///         - 셀랙터 UI 목록 업데이트
    ///         - Orb Data 매니저 업데이트
    ///         - SolarSystem의 해당하는 Orb Update
    ///         - 서버데이터 업데이트
    /// 3. 서버에 의한 데이터 변경(씬 재구성)
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
    /// 가지고 있는 객체들 기능 분류
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
    /// 옵저버
    /// </summary>
    public override void OnNotify()
    {
        
    }
}
