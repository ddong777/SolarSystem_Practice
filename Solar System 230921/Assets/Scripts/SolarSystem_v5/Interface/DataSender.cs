using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSender : MonoBehaviour, ISender
{
    public List<IReceiver> receivers = new List<IReceiver>();

    public virtual void Attach(IReceiver receiver)
    {
        //Debug.Log("Attach: " + this.GetType().Name + "에 " + receiver.GetType().Name + " 등록");
        receivers.Add(receiver);
    }

    public virtual void Detach(IReceiver receiver)
    {
        //Debug.Log(this.GetType().Name + "에서 " + receiver.GetType().Name + " 제거");
        receivers.Remove(receiver);
    }

    public virtual void SendData()
    {
        foreach (IReceiver receiver in receivers)
        {
            //Debug.Log(this.GetType().Name + "에서 " + receiver.GetType().Name + "로 데이터 전송");
            receiver.ReceiveData(this);
        }
    }
}
