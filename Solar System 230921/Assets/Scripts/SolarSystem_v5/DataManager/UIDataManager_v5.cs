using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDataManager_v5 : _DataManager_v5
{
    // �ʿ� ������
    // OrbData
    // List<OrbData.orbType>
    // Orb Pos/Rot
    // OrbTrn
    // isMaster

    public override void Notify()
    {
        foreach (IObserver ob in observers)
        {
            ob.OnNotify();
        }
    }
}
