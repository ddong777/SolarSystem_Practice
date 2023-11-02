using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private OrbDataManager_v4 datamanager;

    private GameObject starUIPrefab;
    private GameObject planetUIPrefab;

    [Header("Orb Data Editor")]
    public Dropdown orbTypeDrd;

    public InputField posXInF;
    public InputField rotZInF;
    public InputField sizeInF;

    public Dropdown spinDirDrd;
    public InputField spinSpeedInF;

    public Dropdown orbitDirDrd;
    public InputField OrbitSpeedInF;

    public Button applyBtn;

    [Header("Solar System Editor")]
    public Transform solarSystemOrbsTrn;
    public List<Button> orbBtnList = new List<Button>();
    public Button addBtn;

    [Header("NowOrb Data Panel")]
    public Text orbPosTxt;
    public Text camPosTxt;

    [Header("Exit")]
    public Button exitBtn;

    [Space(10)]
    public GameObject clientTxt;
    public Toggle camSyncTgl;
    public bool isCamSync = true;

    [Space(10)]
    // 현재 선택중인 천체ID
    public int nowOrbID = 0;

    public void Init()
    {
        datamanager = GameManager_v4.Inst.DataManager;

        starUIPrefab = Resources.Load<GameObject>("Prefabs/Templete/V4/Star_UI");
        planetUIPrefab = Resources.Load<GameObject>("Prefabs/Templete/V4/Planet_UI");

        if (!GameManager_v4.Inst.SyncManager.IsMasterClient)
        {
            nowOrbID = GameManager_v4.Inst.SyncManager.GetCustomProperty_Int(PropertyKey.OrbID);
        }

        exitBtn.onClick.AddListener(GameManager_v4.Inst.NetworkManager.LeaveRoom);
        camSyncTgl.onValueChanged.AddListener((m_Toggle) => { ToggleCamSync(m_Toggle); });
    }

    // 천체데이터관리창 접근 관리
    // 마스터 클라이언트만 수정/적용 가능
    public void SetAccess_OrbdataEditor(bool value)
    {
        orbTypeDrd.interactable = value;

        posXInF.interactable = value;
        rotZInF.interactable = value;
        sizeInF.interactable = value;

        spinDirDrd.interactable = value;
        spinSpeedInF.interactable = value;

        orbitDirDrd.interactable = value;
        OrbitSpeedInF.interactable = value;

        applyBtn.gameObject.SetActive(value);

        clientTxt.gameObject.SetActive(value);
        camSyncTgl.gameObject.SetActive(value);
    }

    // 천체데이터관리창 초기화
    public void InitOrbDataEditor()
    {
        orbTypeDrd.ClearOptions();
        List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();
        foreach (string type in Enum.GetNames(typeof(OrbType)))
        {
            Dropdown.OptionData optionData = new Dropdown.OptionData();
            optionData.text = type;
            optionDatas.Add(optionData);
        }
        orbTypeDrd.AddOptions(optionDatas);
    }

    // 천체데이터관리창 세팅
    public void SetOrbDataEditor(int id)
    {
        orbTypeDrd.value = (int)datamanager.orbDatas[id].orbType;

        posXInF.text = datamanager.orbDatas[id].orbPosX.ToString();
        rotZInF.text = datamanager.orbDatas[id].orbRotZ.ToString();
        sizeInF.text = datamanager.orbDatas[id].orbSize.ToString();

        spinDirDrd.value = (int)datamanager.orbDatas[id].spinDir;
        spinSpeedInF.text = datamanager.orbDatas[id].spinSpeed.ToString();
        
        orbitDirDrd.value = (int)datamanager.orbDatas[id].orbitDir;
        OrbitSpeedInF.text = datamanager.orbDatas[id].orbitSpeed.ToString();

        applyBtn.onClick.RemoveAllListeners();
        applyBtn.onClick.AddListener(Update_OrbDataEditor);
    }

    // 천체목록창 값 세팅
    public void SetSolarSystemEditor()
    {
        for (int i = 0; i < solarSystemOrbsTrn.childCount; i++)
        {
            Destroy(solarSystemOrbsTrn.GetChild(i).gameObject);
        }

        for (int i = 0; i < datamanager.orbDatas.Count; i++)
        {
            OrbData data = datamanager.orbDatas[i];

            GameObject orbObj;
            if (data.isCenterOrb == true)
                orbObj = Instantiate(starUIPrefab, solarSystemOrbsTrn);
            else
                orbObj = Instantiate(planetUIPrefab, solarSystemOrbsTrn);

            orbObj.transform.GetComponentsInChildren<Text>()[0].text = data.orbType.ToString();
            orbObj.transform.GetComponentsInChildren<Text>()[1].text = data.id.ToString();

            Button btn = orbObj.transform.GetComponent<Button>();
            btn.onClick.AddListener(() => { GetNowOrb_OnClick(btn); });
            orbBtnList.Add(btn);
        }
    }

    // 천체데이터관리창 업데이트
    public void Update_OrbDataEditor()
    {
        OrbData data = new OrbData();
        data.orbType = (OrbType)Enum.ToObject(typeof(OrbType), orbTypeDrd.value);
        data.orbPrefab = Resources.Load<GameObject>($"Prefabs/OrbFeature/{orbTypeDrd.value}_{orbTypeDrd.value}Feature");
        data.orbPosX = float.Parse(posXInF.text);
        data.orbRotZ = float.Parse(rotZInF.text);
        data.orbSize = float.Parse(sizeInF.text);
        data.spinDir = (MoveDir)Enum.ToObject(typeof(MoveDir), spinDirDrd.value);
        data.spinSpeed = float.Parse(spinSpeedInF.text);
        data.orbitDir = (MoveDir)Enum.ToObject(typeof(MoveDir), orbitDirDrd.value);
        data.orbitSpeed = float.Parse(OrbitSpeedInF.text);
        
        datamanager.UpdateData(data, nowOrbID);
        Update_SolarSystemEditor();
        GameManager_v4.Inst.SyncManager.SetRoomCustomPropertyData(PropertyKey.OrbData);
        GameManager_v4.Inst.SolarSystem.EditOrb(nowOrbID);

        GameManager_v4.Inst.UpdateCam();
    }

    // 천체목록 데이터창 업데이트
    public void Update_SolarSystemEditor()
    {
        orbBtnList[nowOrbID].GetComponentsInChildren<Text>()[0].text = datamanager.orbDatas[nowOrbID].orbType.ToString();
    }

    // 선택중인 천체, 카메라 위치 정보 표시
    public void UpdateDataPanel(Vector3 orbPos, Vector3 camPos)
    {
        orbPosTxt.text = orbPos.ToString();
        camPosTxt.text = camPos.ToString();
    }

    // 현재 선택중인 천체 ID 얻고,
    // 천체데이터관리창 업데이트
    public void GetNowOrb_OnClick(Button btn)
    {
        nowOrbID = int.Parse(btn.transform.GetComponentsInChildren<Text>()[1].text);

        ClickOrb_SolarSystemEditor();
    }

    public void ClickOrb_SolarSystemEditor()
    {
        SetOrbDataEditor(nowOrbID);

        GameManager_v4.Inst.UpdateCam();

        // 카메라 동기화 true 면 다른 클라이언트 카메라 업데이트
        if (camSyncTgl.isOn)
        {
            GameManager_v4.Inst.SyncManager.SetRoomCustomPropertyData(PropertyKey.OrbID);
        }
    }

    public void ToggleCamSync(bool change)
    {
        if (camSyncTgl.isOn)
        {
            GameManager_v4.Inst.SyncManager.SetRoomCustomPropertyData(PropertyKey.OrbID);
        }
    }
}
