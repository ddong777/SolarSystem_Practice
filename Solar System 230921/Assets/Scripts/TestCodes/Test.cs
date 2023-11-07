using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Test : MonoBehaviourPunCallbacks
{

    private void Start()
    {
        test();
    }
    void test()
    {
        OrbData data = new OrbData();
        Converter converter = new Converter();
    }
}
