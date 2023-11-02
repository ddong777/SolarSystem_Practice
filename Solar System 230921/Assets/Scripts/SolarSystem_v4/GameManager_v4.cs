using UnityEngine;

public class GameManager_v4 : MonoBehaviour
{
    private static GameManager_v4 instance;
    public static GameManager_v4 Inst => instance;

    [SerializeField] private SolarSystemController_v4 solarSystem;
    public SolarSystemController_v4 SolarSystem => solarSystem;

    private NetworkManager networkManager;
    public NetworkManager NetworkManager => networkManager;

    private SyncManager syncManager;
    public SyncManager SyncManager => syncManager;

    private OrbDataManager_v4 dataManager;
    public OrbDataManager_v4 DataManager => dataManager;

    private UIManager uiManager;
    public UIManager UIManager => uiManager;

    private CameraController cam;
    public CameraController Cam => cam;

    // 현재 선택중인 천체ID
    public int NowOrbID 
    {
        get => UIManager.nowOrbID; 
        set => UIManager.nowOrbID = value;
    }

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

    public void Init()
    {
        solarSystem = FindAnyObjectByType<SolarSystemController_v4>();
        syncManager = FindObjectOfType<SyncManager>();
        networkManager = FindAnyObjectByType<NetworkManager>();
        dataManager = FindAnyObjectByType<OrbDataManager_v4>();
        uiManager = FindAnyObjectByType<UIManager>();
        cam = FindAnyObjectByType<CameraController>();

        syncManager.Init();
        networkManager.Init_OnMain();
        uiManager.InitOrbDataEditor();

        dataManager.Init();
        solarSystem.Init();
        uiManager.Init();
    }

    public void OnEnable()
    {
        Init();
        Set();
    }

    public void Set()
    {
        uiManager.SetAccess_OrbdataEditor(syncManager.StartInOfflineMode);

        solarSystem.CreatOrbs();

        uiManager.SetOrbDataEditor(0);
        uiManager.SetSolarSystemEditor();
        uiManager.SetAccess_OrbdataEditor(syncManager.IsMasterClient);

        solarSystem.SetOrbs();

        cam.SetTarget(SolarSystem.orbs[NowOrbID].transform, SolarSystem.orbs[NowOrbID].orbData.orbSize);

        if (syncManager.IsMasterClient)
        {
            syncManager.SetRoomCustomPropertyData(PropertyKey.OrbData); // set orbData and ID
            syncManager.SetRoomCustomPropertyData(PropertyKey.OrbPosList); // set Pos/Rot
        }
        else
        {
            SolarSystem.UpdateOrbsTrnFromServer(
                    syncManager.GetCustomProperty_Vec3Array(PropertyKey.OrbPosList),
                    syncManager.GetCustomProperty_Vec3Array(PropertyKey.OrbRotList));
        }
    }

    private void Update()
    {
        if (solarSystem != null)
            solarSystem.MoveOrbs();
        UIManager.UpdateDataPanel(solarSystem.orbs[NowOrbID].transform.localPosition, cam.transform.position);
    }

    public void UpdateAllThings()
    {
        dataManager.UpdateOrbDataFromServer(syncManager.GetCustomProperty_stringArray(PropertyKey.OrbData));
        uiManager.SetOrbDataEditor(syncManager.GetCustomProperty_Int(PropertyKey.OrbID));
        uiManager.Update_SolarSystemEditor();
        SolarSystem.EditOrb(NowOrbID);
    }

    public void UpdateUI()
    {
        if (syncManager.GetCustomProperty_Int(PropertyKey.OrbID) == NowOrbID) return;

        NowOrbID = syncManager.GetCustomProperty_Int(PropertyKey.OrbID);
        uiManager.ClickOrb_SolarSystemEditor();
    }

    public void UpdateOrbPosAndRot()
    {
        SolarSystem.UpdateOrbsTrnFromServer(
                    syncManager.GetCustomProperty_Vec3Array(PropertyKey.OrbPosList),
                    syncManager.GetCustomProperty_Vec3Array(PropertyKey.OrbRotList));
    }

    public void UpdateCam()
    {
        syncManager.SetRoomCustomPropertyData(PropertyKey.OrbPosList);
        Orb_v3 nowOrb = SolarSystem.orbs[NowOrbID];
        Cam.SetTarget(nowOrb.transform, nowOrb.orbData.orbSize);
    }
}
