using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class SyncManager_v5 : MonoBehaviourPunCallbacks, ISender, IReceiver
{
    public bool isTestMode = true;
    public bool StartInOfflineMode => PhotonNetwork.PhotonServerSettings.StartInOfflineMode;
    public bool IsMasterClient => PhotonNetwork.IsMasterClient;

    public Hashtable CustomPropeties {
        get
        {
            if (PhotonNetwork.CurrentRoom == null)
            {
                return null;
            }
            else
            {
                return PhotonNetwork.CurrentRoom.CustomProperties;
            }
        }
        set
        {
            if (value != null && value != PhotonNetwork.CurrentRoom.CustomProperties)
            {
                PhotonNetwork.CurrentRoom.SetCustomProperties(value);
            }
        }
    }

    private Timer timer = new Timer();

    public void Init()
    {
        timer.InitTimer(10);
        timer.OnTimeDone += () => { 
            // ��ġ ����ȭ
        };
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        CustomPropeties = propertiesThatChanged; 
    }

    //=========================================================================
    //=========================================================================

    public List<IReceiver> receivers = new List<IReceiver>();

    public void Attach(IReceiver receiver)
    {
        Debug.Log("Attach: " + this.GetType().Name + "�� " + receiver.GetType().Name + " ���");
        receivers.Add(receiver);
    }

    public void Detach(IReceiver receiver)
    {
        Debug.Log(this.GetType().Name + "���� " + receiver.GetType().Name + " ����");
        receivers.Remove(receiver);
    }

    public void SendData()
    {
        foreach (IReceiver receiver in receivers)
        {
            Debug.Log(this.GetType().Name + "���� " + receiver.GetType().Name + "�� ������ ����");
            receiver.ReceiveData(this);
        }
    }

    public void ReceiveData(ISender _sender)
    {
        switch (_sender)
        {
            case NetworkData_v5 sender:
                if (IsMasterClient)
                {
                    CustomPropeties = sender.CustomPropeties;
                }
                break;
            default:
                break;
        }
    }
}
