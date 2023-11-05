using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReceiver 
{
    public void ReceiveData(ISender sender);
}
