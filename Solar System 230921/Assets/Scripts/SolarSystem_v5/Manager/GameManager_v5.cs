using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_v5 : _Manager_v5
{
    protected Converter converter = new Converter();

    private static GameManager_v5 instance;

    private OrbDataManager_v5 orbDataManager;
    private OrbFactory orbFactory;
    private SolarSystemController_v5 solarSystem;

    private UIDataManager_v5 uiDataManager;
    private UIManager_v5 uiManager;

    private NetworkDataManager_v5 networkDataManager;
    private NetworkManager_v5 networkManager;

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
    /// 1. 초기화 (씬 구성)
    /// 2. 데이터 변경(씬 재구성)
    /// </summary>
    private void Init()
    {
        orbDataManager = FindObjectOfType<OrbDataManager_v5>();
        solarSystem = FindObjectOfType<SolarSystemController_v5>();

        uiDataManager = FindObjectOfType<UIDataManager_v5>();
        uiManager = FindObjectOfType<UIManager_v5>();

        networkDataManager = FindObjectOfType<NetworkDataManager_v5>();
        networkManager = FindObjectOfType<NetworkManager_v5>() ;
    }
    private void Set()
    {
        SetOrbDataManager();
        SetSolarSystem();

        SetUIDataManager();
        SetUIManager();
    }

    /// <summary>
    /// 2. 데이터 변경(씬 재구성)
    ///     1) 에디터를 통한 orb 데이터 변경
    ///         - 에디터 UI 업데이트
    ///         - 셀랙터 UI 목록 업데이트(OrbType 바뀌었을 경우에만)
    ///         - Orb Data 매니저 (데이터) 업데이트
    ///         - SolarSystem의 해당하는 Orb Update
    ///         - 서버데이터 업데이트(Orb data)
    ///     2) 셀랙터를 통한 Orb 선택 
    ///         - 에디터 UI 업데이트(선택한 Orb의 데이터로)
    ///         - Cam target 변경
    ///         - SingleUI의 orb pos 대상 변경
    ///         - 서버데이터 업데이트
    ///     3) Cam sync 토글
    ///         - On : 서버데이터 받아와서 모든 화면 싱크
    ///         - Off : Orb data, Orb Pos/Rot만 동기화
    /// </summary>
    private void UserAction_OrbChanged()
    {

    }
    private void UserAction_OrbSelected()
    {

    }
    private void UserAction_CamSyncToggle()
    {

    }
    
    /// <summary>
    /// 가지고 있는 객체들 기능 분류
    /// </summary>
    private void SetOrbDataManager()
    {
        orbDataManager.Attach(this);
        orbDataManager.Attach(solarSystem);

        if (networkManager.isMaster)
        {
            orbDataManager.Set();
        }
        else
        {
            // orbDataManager.Set(customPropertiesDataManager.GetCustomProperty_stringArray(PropertyKey.OrbData));
        }
    }

    private void UpdateOrbDataManager()
    {

    }

    private void SetSolarSystem()
    {
        solarSystem.SetCapacity(orbDataManager.orbDatas.Count);
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

    private void SetUIDataManager()
    {
        uiDataManager.Attach(this);
        uiDataManager.Attach(uiManager);

        // 이렇게 하면 안됨 옵저버 이용하기
        uiDataManager.SelectorUIData_OrbNameList = converter.OrbTypeString(orbDataManager.orbDatas);
        uiDataManager.EditorUIData_NowOrbData = converter.OrbDataToEditorData(orbDataManager.orbDatas[uiDataManager.NowOrbID], uiDataManager.EditorUIData_NowOrbData);
        uiDataManager.SingleUIData_NowOrbTrn = solarSystem.GetOrb(uiDataManager.NowOrbID).transform;
    }

    private void UpdateUIDataManager()
    {

    }

    private void SetUIManager()
    {
        // 이렇게 하면 안됨 옵저버 이용하기
        uiManager.SetAccess(networkManager.isMaster);

        uiManager.Set_OrbSelector(uiDataManager.SelectorUIData_OrbNameList);
        uiManager.Set_OrbDataEditor(uiDataManager.EditorUIData_NowOrbData);
        uiManager.Set_SingleUIs(uiDataManager.SingleUIData_NowOrbTrn);
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
