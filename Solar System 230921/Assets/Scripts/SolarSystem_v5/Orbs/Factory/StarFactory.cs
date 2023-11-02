using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFactory : OrbFactory
{
    public GameObject starTemplete;

    public override void SetPrefabs()
    {
        base.SetPrefabs();
        if (starTemplete == null)
        {
            starTemplete = Resources.Load<GameObject>($"Prefabs/Templete/V5/StarTrn");
        }
    }

    public override Orb_v5 Create(OrbData _data)
    {
        GameObject temp = Instantiate(starTemplete, parentTrn);

        Star_v5 _star = temp.GetComponent<Star_v5>();

        _star.SetData(_data);
        _star.SetFeature(orbPrefabs[_star.data.orbType]);

        SetStarChild(_star);
        SetStarTrn(_star);

        _star.InitMove();

        return _star;
    }

    public void SetStarChild(Star_v5 _star)
    {
        if (_star.childenTrn == null)
        {
            _star.childenTrn = _star.transform.GetChild(0).Find("Planets");
        }
    }

    public void SetStarTrn(Star_v5 _star)
    {
        Vector3 _pos = Vector3.zero;
        Vector3 _rot = new Vector3(0, 0, _star.data.orbRotZ);
        _star.SetTransform(_pos, _rot);
    }
}
