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
    /// 1. �ʱ�ȭ (�� ����)
    /// 2. ������ ����(�� �籸��)
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
    /// 2. ������ ����(�� �籸��)
    ///     1) �����͸� ���� orb ������ ����
    ///         - ������ UI ������Ʈ
    ///         - ������ UI ��� ������Ʈ(OrbType �ٲ���� ��쿡��)
    ///         - Orb Data �Ŵ��� (������) ������Ʈ
    ///         - SolarSystem�� �ش��ϴ� Orb Update
    ///         - ���������� ������Ʈ(Orb data)
    ///     2) �����͸� ���� Orb ���� 
    ///         - ������ UI ������Ʈ(������ Orb�� �����ͷ�)
    ///         - Cam target ����
    ///         - SingleUI�� orb pos ��� ����
    ///         - ���������� ������Ʈ
    ///     3) Cam sync ���
    ///         - On : ���������� �޾ƿͼ� ��� ȭ�� ��ũ
    ///         - Off : Orb data, Orb Pos/Rot�� ����ȭ
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
    /// ������ �ִ� ��ü�� ��� �з�
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

        // �̷��� �ϸ� �ȵ� ������ �̿��ϱ�
        uiDataManager.SelectorUIData_OrbNameList = converter.OrbTypeString(orbDataManager.orbDatas);
        uiDataManager.EditorUIData_NowOrbData = converter.OrbDataToEditorData(orbDataManager.orbDatas[uiDataManager.NowOrbID], uiDataManager.EditorUIData_NowOrbData);
        uiDataManager.SingleUIData_NowOrbTrn = solarSystem.GetOrb(uiDataManager.NowOrbID).transform;
    }

    private void UpdateUIDataManager()
    {

    }

    private void SetUIManager()
    {
        // �̷��� �ϸ� �ȵ� ������ �̿��ϱ�
        uiManager.SetAccess(networkManager.isMaster);

        uiManager.Set_OrbSelector(uiDataManager.SelectorUIData_OrbNameList);
        uiManager.Set_OrbDataEditor(uiDataManager.EditorUIData_NowOrbData);
        uiManager.Set_SingleUIs(uiDataManager.SingleUIData_NowOrbTrn);
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
