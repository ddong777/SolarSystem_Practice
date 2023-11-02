using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb_v2 : MonoBehaviour
{
    // Orb 기본 설정
    public Vector3 orbPos;
    public Vector3 orbRot;
    public float orbSize;
    public GameObject orbPrefab;

    
    public virtual void Init()
    {
        InitTransform();
        // Debug.Log("init orb");
    }

    public virtual void Move() { }

    private void InitTransform()
    {
        transform.localPosition = orbPos;
        transform.localRotation = Quaternion.Euler(orbRot);

        GameObject orbFeature = Instantiate(orbPrefab, transform);
        orbFeature.transform.localScale = new Vector3(orbSize, orbSize, orbSize);
    }
}