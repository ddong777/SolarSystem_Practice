using System;
using UnityEngine;

[Serializable] 
public class OrbData
{
    public int id;
    [Rename("천체 타입")]
    public OrbType orbType;
    public GameObject orbPrefab;

    [Space(10)]
    public bool isCenterOrb;

    [Space(10)]

    [Rename("중심점과의 거리")]
    public float orbPosX;
    [Rename("천체 기울기")]
    public float orbRotZ;
    [Rename("천체 크기")]
    public float orbSize;

    [Space(10)]

    [Rename("자전 방향")]
    public MoveDir spinDir;
    [Rename("자전 속도")]
    public float spinSpeed;
    [Rename("공전 방향")]
    public MoveDir orbitDir;
    [Rename("공전 속도")]
    public float orbitSpeed;
}

