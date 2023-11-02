using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb_v1 : MonoBehaviour
{
    [Header("ÀÚÀü")]
    public float spinSpeed = 100.0f;
    public bool isSpinClockwise = true;

    void Update()
    {
        Spin();
    }

    protected void Spin()
    {
        if (isSpinClockwise)
            transform.Rotate(spinSpeed * Time.deltaTime * Vector3.up);
        else
            transform.Rotate(-spinSpeed * Time.deltaTime * Vector3.up);
    }
}
