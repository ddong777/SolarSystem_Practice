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
    [Rename("����ȭ ����(��)")]
    public float syncValue = 10f; // 10�ʿ� �ѹ� ����ȭ

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

    // �� ������ ����
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
        Debug.Log($"���忩�� : ({PhotonNetwork.IsMasterClient}). �� Ŀ���� ������Ƽ�� ������Ʈ�մϴ�.");

        // �Խ�Ʈ Ŭ���̾�Ʈ�� �¾�� ������Ʈ
        if (IsMasterClient) return;

        customPropeties = propertiesThatChanged;

        // ���� ������ ����
        if (GetCustomProperty_Int(PropertyKey.ID) == 0)
        {
            GameManager_v4.Inst.UpdateAllThings();
        }

        // ���� ������ ����
        else if (GetCustomProperty_Int(PropertyKey.ID) == 1)
        {
            GameManager_v4.Inst.UpdateUI();
        }

        // õü ��ġ ������Ʈ
        else if (GetCustomProperty_Int(PropertyKey.ID) == 2)
        {
            GameManager_v4.Inst.UpdateOrbPosAndRot();
        }
    }

    public int GetCustomProperty_Int(PropertyKey key)
    {
        if (!customPropeties.ContainsKey(key.ToString()))
        {
            Debug.Log($"{key}�� ���� Ű�� �Դϴ�.");
        }
        return (int)customPropeties[key.ToString()];
    }
    public string[] GetCustomProperty_stringArray(PropertyKey key)
    {
        if (!customPropeties.ContainsKey(key.ToString()))
        {
            Debug.Log($"{key}�� ���� Ű�� �Դϴ�.");
        }
        return (string[])customPropeties[key.ToString()];
    }
    public Vector3[] GetCustomProperty_Vec3Array(PropertyKey key)
    {
        if (!customPropeties.ContainsKey(key.ToString()))
        {
            Debug.Log($"{key}�� ���� Ű�� �Դϴ�.");
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
        Debug.Log($"����ȭ �߻�. Ÿ�̸� �ʱ�ȭ : {syncCount}");
        syncCount = syncValue; // �ֱ⸦ �ð��� ���缭 ( 1�ʿ� �ѹ�, Ȥ�� �� �����Ӵ� �ѹ� ������)
    }

    private void RunTimer()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        if (syncCount > 0)
        {
            syncCount -= Time.deltaTime; // Time.deltaTime = 1�� / �ʴ� ������ (1�����Ӵ� ����ð�)
        }
        else
        {
            SetRoomCustomPropertyData(PropertyKey.OrbPosList);
            InitTimer();
        }
    }
}
