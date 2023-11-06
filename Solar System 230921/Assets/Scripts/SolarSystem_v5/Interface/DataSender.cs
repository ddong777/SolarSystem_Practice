using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSender : MonoBehaviour, ISender
{
    public List<IReceiver> receivers = new List<IReceiver>();

    public virtual void Attach(IReceiver receiver)
    {
        //Debug.Log("Attach: " + this.GetType().Name + "�� " + receiver.GetType().Name + " ���");
        receivers.Add(receiver);
    }

    public virtual void Detach(IReceiver receiver)
    {
        //Debug.Log(this.GetType().Name + "���� " + receiver.GetType().Name + " ����");
        receivers.Remove(receiver);
    }

    public virtual void SendData()
    {
        foreach (IReceiver receiver in receivers)
        {
            //Debug.Log(this.GetType().Name + "���� " + receiver.GetType().Name + "�� ������ ����");
            receiver.ReceiveData(this);
        }
    }
}
