using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_v5 : _Manager_v5
{
    protected Converter converter = new Converter();

    private static GameManager_v5 instance;

    DataManager_v5 dataManager;

    private SolarSystemData_v5 orbDataManager;
    private SolarSystemController_v5 solarSystem;

    private UIData_v5 uiDataManager;
    private UIManager_v5 uiManager;

    private NetworkData_v5 networkDataManager;
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
        dataManager = FindObjectOfType<DataManager_v5>();

        solarSystem = FindObjectOfType<SolarSystemController_v5>();

        uiDataManager = FindObjectOfType<UIData_v5>();
        uiManager = FindObjectOfType<UIManager_v5>();

        networkDataManager = FindObjectOfType<NetworkData_v5>();
        networkManager = FindObjectOfType<NetworkManager_v5>() ;

        dataManager.Init();
    }
    private void Set()
    {
        Resister();

        dataManager.Set();

        // SetUIDataManager();
        // SetUIManager();
    }

    private void Resister()
    {
        dataManager.solarData.Attach(solarSystem);
    }
    
    /// <summary>
    /// 가지고 있는 객체들 기능 분류
    /// </summary>
    private void SetUIDataManager()
    {
        // 이렇게 하면 안됨 옵저버 이용하기
        //uiDataManager.SelectorUIData_OrbNameList = converter.OrbTypeString(orbDataManager.OrbDatas);
        //uiDataManager.EditorUIData_NowOrbData = converter.OrbDataToEditorData(orbDataManager.OrbDatas[uiDataManager.NowOrbID], uiDataManager.EditorUIData_NowOrbData);
        //uiDataManager.SingleUIData_NowOrbTrn = solarSystem.GetOrb(uiDataManager.NowOrbID).transform;
    }

    private void SetUIManager()
    {
        // 이렇게 하면 안됨 옵저버 이용하기
        //uiManager.SetAccess(networkManager.isMaster);

        //uiManager.Set_OrbSelector(uiDataManager.SelectorUIData_OrbNameList);
        //uiManager.Set_OrbDataEditor(uiDataManager.EditorUIData_NowOrbData);
        //uiManager.Set_SingleUIs(uiDataManager.SingleUIData_NowOrbTrn);
    }

    /// <summary>
    /// 옵저버
    /// </summary>
    public override void ReceiveData()
    {
        
    }
}
