using System;
using UnityEngine;

[Serializable] 
public class OrbData
{
    public int id;
    [Rename("õü Ÿ��")]
    public OrbType orbType;
    public GameObject orbPrefab;

    [Space(10)]
    public bool isCenterOrb;

    [Space(10)]

    [Rename("�߽������� �Ÿ�")]
    public float orbPosX;
    [Rename("õü ����")]
    public float orbRotZ;
    [Rename("õü ũ��")]
    public float orbSize;

    [Space(10)]

    [Rename("���� ����")]
    public MoveDir spinDir;
    [Rename("���� �ӵ�")]
    public float spinSpeed;
    [Rename("���� ����")]
    public MoveDir orbitDir;
    [Rename("���� �ӵ�")]
    public float orbitSpeed;
}

