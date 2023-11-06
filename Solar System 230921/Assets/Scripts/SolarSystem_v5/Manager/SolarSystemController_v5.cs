using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemController_v5 : MonoBehaviour
{
    private OrbFactory orbFactory;

    public bool isCreated = false;

    public Star_v5 star;
    public List<Orb_v5> orbs;

    public void Create(List<OrbData> datas)
    {
        SetCapacity(datas.Count);
        isCreated = true;
        foreach (OrbData data in datas)
        {
            if (data.isCenterOrb)
            {
                orbFactory = new StarFactory();
                orbFactory.Set(transform);

                SetStar(orbFactory.Create(data) as Star_v5);
            }
            else
            {
                orbFactory = new PlanetFactory();
                orbFactory.Set(star.childenTrn);

                SetOrb(orbFactory.Create(data));
            }
        }
    }

    public void SetCapacity(int capacity)
    {
        orbs.Capacity = capacity;
    }

    public void SetOrb(Orb_v5 _orb)
    {
        orbs.Add(_orb);
    }

    public void SetStar(Star_v5 _star)
    {
        star = _star;
        orbs.Add(_star);
    }

    public void UpdateOrbData(int _id, OrbData _data)
    {
        orbs[_id].data = _data;
    }
    public void UpdateOrbData(int _id, GameObject _prefab)
    {
        orbs[_id].data.orbPrefab = _prefab;
    }

    public void UpdateOrbTrn(int _id, Vector3 _pos, Vector3 _rot)
    {
        orbs[_id].Position = _pos;
        orbs[_id].Rotation = _rot;
    }

    public void UpdateAllOrbData(List<OrbData> _datas)
    {
        for (int i = 0; i < orbs.Count; i++)
        {
            orbs[i].data = _datas[i]; // 메모리 해제 방법 찾아서 적용하기
        }
    }

    public void UpdateAllOrb_PosNRot(Vector3[] _poss, Vector3[] _rots)
    {
        for (int i = 0; i < orbs.Count; i++)
        {
            orbs[i].Position = _poss[i];
            orbs[i].Rotation = _rots[i];
        }
    }

    public Orb_v5 GetOrb(int _id)
    {
        return orbs[_id];
    }

    public void MoveAllOrbs()
    {
        foreach (Orb_v5 _orb in orbs)
        {
            _orb.Move();
        }
    }
}
