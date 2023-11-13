using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_v5 : MonoBehaviour
{
    private static GameManager_v5 instance;

    private SolarSystemController_v5 solarSystem;
    private UIManager_v5 uiManager;
    private EventManager_v5 eventManager;
    private CamController_v5 cameraController;

    private SyncManager_v5 serverSyncManager;
    private NetworkManager_v5 networkManager;

    private void Awake()
    {
        //Screen.SetResolution(960, 540, false);
        Screen.SetResolution(800, 450, false);

        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(this.gameObject);
        }
    }

    public void OnEnable()
    {
        Init();
        Set();
    }

    private void Init()
    {
        serverSyncManager = FindObjectOfType<SyncManager_v5>();
        networkManager = FindObjectOfType<NetworkManager_v5>();

        solarSystem = FindObjectOfType<SolarSystemController_v5>();
        uiManager = FindObjectOfType<UIManager_v5>();
        eventManager = FindObjectOfType<EventManager_v5>();
        cameraController = FindObjectOfType<CamController_v5>();

        serverSyncManager.Init();
        networkManager.Init(serverSyncManager);

        solarSystem.Init();
        uiManager.Init();
        cameraController.Init();
    }

    private void Set()
    {
        solarSystem.Set();
        uiManager.Set();
        cameraController.Set();

        serverSyncManager.Set();

        // 이벤트에 매니저 함수들 등록
        eventManager.resetEvents();
        
        eventManager.SetEvent("exit", networkManager.LeaveRoom);

        eventManager.SetEvent("isMaster", () => { Debug.Log("Event: isMaster 값 변경"); });
        eventManager.SetEvent("isSyncMode", () => { Debug.Log("Event: isSyncMode 값 변경"); });
        eventManager.SetEvent("nowOrbID", () => { Debug.Log("Event: nowOrbID 값 변경"); });
        eventManager.SetEvent("orbDatas", () => { Debug.Log("Event: orbDatas 값 변경"); });
        eventManager.SetEvent("orbTrns", () => { Debug.Log("Event: orbTrns 값 변경"); });

        eventManager.AddEvent("nowOrbID", cameraController.Set);
        eventManager.AddEvent("nowOrbID", serverSyncManager.Send_FromMaster);

        eventManager.AddEvent("orbDatas", solarSystem.UpdateAllOrb);
        eventManager.AddEvent("orbDatas", serverSyncManager.Send_FromMaster);

        eventManager.AddEvent("isSyncMode", serverSyncManager.Send_FromMaster);
        eventManager.AddEvent("isSyncMode", serverSyncManager.SendFromClient);
    }
}
