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

    public void OnEnable()
    {
        if (!PhotonNetwork.IsConnected) return;
        Init();
        Set();
    }

    private void Init()
    {
        serverSyncManager = FindObjectOfType<SyncManager_v5>();

        solarSystem = FindObjectOfType<SolarSystemController_v5>();
        uiManager = FindObjectOfType<UIManager_v5>();
        eventManager = FindObjectOfType<EventManager_v5>();
        cameraController = FindObjectOfType<CamController_v5>();

        serverSyncManager.Init();

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
        eventManager.SetEvent("isMaster", () => { Debug.Log("isMaster 값 변경"); });
        eventManager.SetEvent("isSyncMode", () => { Debug.Log("isSyncMode 값 변경"); });
        eventManager.SetEvent("nowOrbID", () => { Debug.Log("nowOrbID 값 변경"); });
        eventManager.SetEvent("orbDatas", () => { Debug.Log("orbDatas 값 변경"); });
        eventManager.SetEvent("orbTrns", () => { Debug.Log("orbTrns 값 변경"); });

        eventManager.AddEvent("nowOrbID", cameraController.Set);
        eventManager.AddEvent("orbDatas", solarSystem.UpdateAllOrb);

        eventManager.AddEvent("nowOrbID", serverSyncManager.Send_FromMaster);
        eventManager.AddEvent("orbDatas", serverSyncManager.Send_FromMaster);

        eventManager.AddEvent("isSyncMode", serverSyncManager.Send_FromMaster);
        eventManager.AddEvent("isSyncMode", serverSyncManager.SendFromClient);
    }
}
