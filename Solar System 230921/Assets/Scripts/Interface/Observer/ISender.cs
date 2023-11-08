using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISender
{
    public void Attach(IReceiver receiver);
    public void Detach(IReceiver receiver);
    public void SendData<T>(T data);
}
