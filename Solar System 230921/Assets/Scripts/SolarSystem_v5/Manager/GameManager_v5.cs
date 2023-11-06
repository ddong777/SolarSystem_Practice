using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_v5 : MonoBehaviour, IReceiver
{
    protected Converter converter = new Converter();

    private static GameManager_v5 instance;

    private DataManager_v5 dataManager;

    private SolarSystemController_v5 solarSystem;
    private UIManager_v5 uiManager;

    private SyncManager_v5 serverSyncManager;
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
        Resister();
    }
    private void Start()
    {
        
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
        dataManager.Init();

        networkManager = FindObjectOfType<NetworkManager_v5>();

        serverSyncManager = FindObjectOfType<SyncManager_v5>();
        serverSyncManager.Init();

        solarSystem = FindObjectOfType<SolarSystemController_v5>();
        
        uiManager = FindObjectOfType<UIManager_v5>();
        uiManager.Init();
    }
    
    private void Resister()
    {
        serverSyncManager.Attach(dataManager.serverData);

        dataManager.solarData.Attach(this);
        dataManager.uidata.Attach(this);

        dataManager.serverData.Attach(serverSyncManager);

        uiManager.dataEditUI.Attach(dataManager);
        uiManager.orbSelectUI.Attach(dataManager);
        uiManager.singleUI.Attach(dataManager);
    }

    /// <summary>
    /// 옵저버 패턴
    /// </summary>
    public void ReceiveData(ISender _sender)
    {
        switch (_sender)
        {
            case SolarSystemData_v5 sender:
                Debug.Log(solarSystem.GetType().Name + "가 " + sender.GetType().Name + "로부터 데이터 받음");
                if (!solarSystem.isCreated)
                {
                    solarSystem.Create(sender.OrbDatas);
                    foreach (Orb_v5 orb in solarSystem.orbs)
                    {
                        sender.SetData(orb.data.id, orb.data);
                    }
                }
                else
                {
                    solarSystem.UpdateAllOrbData(sender.OrbDatas);
                }

                break;
            case UIData_v5 sender:
                Debug.Log(uiManager.GetType().Name + "가 " + sender.GetType().Name + "로부터 데이터 받음");
                uiManager.SetAccess(sender.IsAccessible);

                uiManager.Set_OrbSelector(sender.SelectorUIData_OrbNameList);
                uiManager.Set_OrbDataEditor(sender.EditorUIData_NowOrbData);
                uiManager.Set_SingleUIs(solarSystem.orbs[sender.NowOrbID].transform, networkManager.LeaveRoom);
                break;
            default:
                break;
        }
    }
}
