using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_v5 : MonoBehaviour
{
    private static GameManager_v5 instance;

    private DataContainer dataManager;

    private SolarSystemController_v5 solarSystem;
    private UIManager_v5 uiManager;

    private SyncManager_v5 serverSyncManager;
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

    }
    private void Start()
    {
        
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
        dataManager = FindObjectOfType<DataContainer>();

        networkManager = FindObjectOfType<NetworkManager_v5>();

        serverSyncManager = FindObjectOfType<SyncManager_v5>();
        serverSyncManager.Init();

        solarSystem = FindObjectOfType<SolarSystemController_v5>();
        
        uiManager = FindObjectOfType<UIManager_v5>();
        uiManager.Init();
    }
}
