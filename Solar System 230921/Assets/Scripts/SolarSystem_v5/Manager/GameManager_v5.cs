using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_v5 : MonoBehaviour
{
    private static GameManager_v5 instance;

    private SolarSystemController_v5 solarSystem;
    private UIManager_v5 uiManager;
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
        cameraController = FindObjectOfType<CamController_v5>();

        solarSystem.Init();
        uiManager.Init();
        cameraController.Init();
    }

    private void Set()
    {
        solarSystem.Set();
        uiManager.Set();
        cameraController.Set();
    }
}
