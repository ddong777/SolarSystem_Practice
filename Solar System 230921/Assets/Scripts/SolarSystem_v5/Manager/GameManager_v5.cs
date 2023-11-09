using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager_v5 : MonoBehaviour
{
    private static GameManager_v5 instance;

    public SolarSystemController_v5 solarSystem;
    public UIManager_v5 uiManager;
    public EventManager_v5 eventManager;
    public CamController_v5 cameraController;

    public SyncManager_v5 serverSyncManager;

    private static bool isInitialized = false;

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
        if (isInitialized) return;
        Init();
    }

    private void Start()
    {
        if (isInitialized) return;
        Set();
    }

    private void Init()
    {
        Debug.Log("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
        
        serverSyncManager = FindObjectOfType<SyncManager_v5>();

        solarSystem = FindObjectOfType<SolarSystemController_v5>();
        uiManager = FindObjectOfType<UIManager_v5>();
        eventManager = FindObjectOfType<EventManager_v5>();
        cameraController = FindObjectOfType<CamController_v5>();

        //serverSyncManager.Init();

        solarSystem.Init();
        uiManager.Init();
        cameraController.Init();
    }

    private void Set()
    {
        solarSystem.Set();
        uiManager.Set();
        cameraController.Set();
        //serverSyncManager.Set();

        // �̺�Ʈ�� �Ŵ��� �Լ��� ���
        eventManager.SetEvent("isMaster", () => { Debug.Log("isMaster �� ����"); });
        eventManager.SetEvent("isSyncMode", () => { Debug.Log("isSyncMode �� ����"); });
        eventManager.SetEvent("nowOrbID", () => { Debug.Log("nowOrbID �� ����"); });
        eventManager.SetEvent("orbDatas", () => { Debug.Log("orbDatas �� ����"); });
        eventManager.SetEvent("orbTrns", () => { Debug.Log("orbTrns �� ����"); });

        eventManager.AddEvent("nowOrbID", cameraController.Set);
        eventManager.AddEvent("orbDatas", solarSystem.UpdateAllOrb);

        isInitialized = true;
    }
}
