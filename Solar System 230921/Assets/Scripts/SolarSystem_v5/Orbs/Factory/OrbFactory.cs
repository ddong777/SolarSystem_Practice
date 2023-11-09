using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OrbType
{
    Sun,
    Mercury,
    Venus,
    Earth,
    Mars,
    Jupiter,
    Saturn,
    Uranus,
    Neptune
}

public abstract class OrbFactory : MonoBehaviour
{
    public Dictionary<OrbType, GameObject> orbPrefabs = new Dictionary<OrbType, GameObject>();

    public Transform parentTrn;

    public void Set(Transform _parent)
    {
        SetPrefabs();
        SetParentTrn(_parent);
    }

    public void SetParentTrn(Transform _parent)
    {
        parentTrn = _parent;
    }

    public virtual void SetPrefabs()
    {
        GameObject[] prefabs = Resources.LoadAll<GameObject>("Prefabs/OrbFeature");
        orbPrefabs = new Dictionary<OrbType, GameObject>(prefabs.Length);
        for (int i = 0; i < prefabs.Length; i++)
        {
            orbPrefabs[(OrbType)i] = prefabs[i];
        }
    }

    public GameObject GetPrefab(OrbType type)
    {
        return orbPrefabs[type];
    }

    public abstract Orb_v5 Create(OrbData _data);
}
