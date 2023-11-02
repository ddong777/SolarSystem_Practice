using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFactory : OrbFactory
{
    public GameObject planetTemplete;

    public override void SetPrefabs()
    {
        base.SetPrefabs();
        if (planetTemplete == null)
        {
            planetTemplete = Resources.Load<GameObject>($"Prefabs/Templete/V5/PlanetTrn");
        }
    }

    public override Orb_v5 Create(OrbData _data)
    {
        GameObject temp = Instantiate(planetTemplete, parentTrn);

        Planet_v5 _planet = temp.GetComponent<Planet_v5>();

        _planet.SetData(_data);
        _planet.SetFeature(orbPrefabs[_planet.data.orbType]);

        SetPlanetCenter(_planet);
        SetPlanetTrn(_planet);

        _planet.InitMove();

        return _planet;
    }

    public void SetPlanetCenter(Planet_v5 _planet)
    {
        if (_planet.centerTrn == null)
        {
            _planet.centerTrn = FindObjectOfType<Star_v5>().transform;
        }
    }

    public void SetPlanetTrn(Planet_v5 _planet)
    {
        float star_r = _planet.centerTrn.localScale.x * 0.5f;
        Vector3 _pos = new Vector3(_planet.data.orbPosX + star_r, 0, 0);
        Vector3 _rot = new Vector3(0, 0, _planet.data.orbRotZ);
        _planet.SetTransform(_pos, _rot);
    }
}
