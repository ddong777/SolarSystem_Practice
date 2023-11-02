using UnityEngine;

using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class SyncManager : MonoBehaviourPunCallbacks
{
    private Hashtable customPropeties;
    public Hashtable CustomPropeties => customPropeties;

    public bool IsMasterClient => PhotonNetwork.IsMasterClient;
    public bool StartInOfflineMode => PhotonNetwork.PhotonServerSettings.StartInOfflineMode;

    private float syncCount;
    [Rename("동기화 간격(초)")]
    public float syncValue = 10f; // 10초에 한번 동기화

    public void Init()
    {
        Debug.Log($"Player GUID : {PhotonNetwork.AuthValues.UserId}");

        InitTimer();
        InitCustomProperty();
    }

    private void InitCustomProperty()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            customPropeties = new Hashtable() { { PropertyKey.ID.ToString(), 0 },
                                                { PropertyKey.OrbData.ToString(), null },
                                                { PropertyKey.OrbID.ToString(), null },
                                                { PropertyKey.OrbPosList.ToString() , null },
                                                { PropertyKey.OrbRotList.ToString(), null },
            };

            PhotonNetwork.CurrentRoom.SetCustomProperties(customPropeties);
        }
        else
        {
            SetCustomPropertiesFromMaster();
        }
    }

    public void SetCustomPropertiesFromMaster()
    {
        customPropeties = PhotonNetwork.CurrentRoom.CustomProperties;
    }

    private void LateUpdate()
    {
        RunTimer();
    }

    // 룸 데이터 설정
    public void SetRoomCustomPropertyData(PropertyKey _key)
    {
        if (!IsMasterClient) return;

        switch (_key)
        {
            case PropertyKey.OrbData:
                SetCustomProperty(PropertyKey.ID, 0);
                SetCustomProperty(PropertyKey.OrbData, GameManager_v4.Inst.DataManager.JsonSerialize_CurrentData());
                SetCustomProperty(PropertyKey.OrbID, GameManager_v4.Inst.NowOrbID);
                break;

            case PropertyKey.OrbID:
                SetCustomProperty(PropertyKey.ID, 1);
                SetCustomProperty(PropertyKey.OrbID, GameManager_v4.Inst.NowOrbID);
                break;

            case PropertyKey.OrbPosList:
                SetCustomProperty(PropertyKey.ID, 2);
                SetCustomProperty(PropertyKey.OrbPosList, GameManager_v4.Inst.SolarSystem.GetOrbsPos());
                SetCustomProperty(PropertyKey.OrbRotList, GameManager_v4.Inst.SolarSystem.GetOrbsRot());
                break;

            default:
                break;
        }

        PhotonNetwork.CurrentRoom.SetCustomProperties(customPropeties);
        InitTimer();
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        Debug.Log($"방장여부 : ({PhotonNetwork.IsMasterClient}). 룸 커스텀 프로퍼티를 업데이트합니다.");

        // 게스트 클라이언트만 태양계 업데이트
        if (IsMasterClient) return;

        customPropeties = propertiesThatChanged;

        // 우측 에디터 수정
        if (GetCustomProperty_Int(PropertyKey.ID) == 0)
        {
            GameManager_v4.Inst.UpdateAllThings();
        }

        // 좌측 에디터 수정
        else if (GetCustomProperty_Int(PropertyKey.ID) == 1)
        {
            GameManager_v4.Inst.UpdateUI();
        }

        // 천체 위치 업데이트
        else if (GetCustomProperty_Int(PropertyKey.ID) == 2)
        {
            GameManager_v4.Inst.UpdateOrbPosAndRot();
        }
    }

    public int GetCustomProperty_Int(PropertyKey key)
    {
        if (!customPropeties.ContainsKey(key.ToString()))
        {
            Debug.Log($"{key}는 없는 키값 입니다.");
        }
        return (int)customPropeties[key.ToString()];
    }
    public string[] GetCustomProperty_stringArray(PropertyKey key)
    {
        if (!customPropeties.ContainsKey(key.ToString()))
        {
            Debug.Log($"{key}는 없는 키값 입니다.");
        }
        return (string[])customPropeties[key.ToString()];
    }
    public Vector3[] GetCustomProperty_Vec3Array(PropertyKey key)
    {
        if (!customPropeties.ContainsKey(key.ToString()))
        {
            Debug.Log($"{key}는 없는 키값 입니다.");
        }
        return (Vector3[])customPropeties[key.ToString()];
    }

    public void SetCustomProperty(PropertyKey key, int _id)
    {
        customPropeties[key.ToString()] = _id;
    }
    public void SetCustomProperty(PropertyKey key, string[] _strings)
    {
        customPropeties[key.ToString()] = _strings;
    }
    public void SetCustomProperty(PropertyKey key, Vector3[] _vec3)
    {
        customPropeties[key.ToString()] = _vec3;
    }

    private void InitTimer()
    {
        Debug.Log($"동기화 발생. 타이머 초기화 : {syncCount}");
        syncCount = syncValue; // 주기를 시간에 맞춰서 ( 1초에 한번, 혹은 몇 프레임당 한번 식으로)
    }

    private void RunTimer()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        if (syncCount > 0)
        {
            syncCount -= Time.deltaTime; // Time.deltaTime = 1초 / 초당 프레임 (1프레임당 실행시간)
        }
        else
        {
            SetRoomCustomPropertyData(PropertyKey.OrbPosList);
            InitTimer();
        }
    }
}
