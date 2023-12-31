using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController_v5 : MonoBehaviour
{
    private Data_Cam data;

    public Transform target;

    public float distance = 40f;

    private void LateUpdate()
    {
        if (target == null) return;

        if (target.position.magnitude > 0)
        {
            transform.position = target.position + new Vector3(0, 0, distance);
            transform.LookAt(target.position);
        }
    }

    public void Init()
    {
        data = FindObjectOfType<Data_Cam>();
        data.Init();
    }

    public void Set()
    {
        SetTarget(data.TargetTrn, data.TargetSize);
    }

    private void SetTarget(Transform trn, float _distance)
    {
        target = trn;

        distance = _distance * -5f;

        if (target.position.magnitude <= 0)
        {
            transform.position = target.position + new Vector3(0, 300, 0);
            transform.LookAt(target.position);
        }
    }
}
