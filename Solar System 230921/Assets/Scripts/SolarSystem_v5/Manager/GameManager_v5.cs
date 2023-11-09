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
    }

    private void Start()
    {
        Set();
    }

    private void Init()
    {
        networkManager = FindObjectOfType<NetworkManager_v5>();
        serverSyncManager = FindObjectOfType<SyncManager_v5>();

        solarSystem = FindObjectOfType<SolarSystemController_v5>();
        uiManager = FindObjectOfType<UIManager_v5>();
        eventManager = FindObjectOfType<EventManager_v5>();
        cameraController = FindObjectOfType<CamController_v5>();

        solarSystem.Init();
        uiManager.Init();
        eventManager.Init();
        cameraController.Init();
    }

    private void Set()
    {
        solarSystem.Set();
        uiManager.Set();
        eventManager.Set();
        cameraController.Set();

        // 이벤트에 매니저 함수들 등록
        eventManager.SetEvent("isMaster", () => { Debug.Log("isMaster Changed"); });
        eventManager.SetEvent("isSyncMode", () => { Debug.Log("isSyncMode Changed"); });
        eventManager.SetEvent("nowOrbID", () => { Debug.Log("nowOrbID Changed"); });
        eventManager.SetEvent("orbDatas", () => { Debug.Log("orbDatas Changed"); });
        eventManager.SetEvent("orbTrns", () => { Debug.Log("orbTrns Changed"); });

        eventManager.AddEvent("nowOrbID", cameraController.Set);
        eventManager.SetEvent("orbDatas", solarSystem.UpdateAllOrb);
    }
}
