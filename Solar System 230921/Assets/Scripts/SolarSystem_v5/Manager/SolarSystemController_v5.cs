using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemController_v5 : _Manager_v5
{
    public Star_v5 star;
    public List<Orb_v5> orbs;

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

    public void UpdateAllOrbTrn(Vector3[] _poss, Vector3[] _rots)
    {
        for (int i = 0; i < orbs.Count; i++)
        {
            orbs[i].Position = _poss[i];
            orbs[i].Rotation = _rots[i];
        }
    }

    public Dictionary<int, Vector3[]> GetOrbTrn(int _id)
    {
        Dictionary<int, Vector3[]> trns = new Dictionary<int, Vector3[]>();
        Vector3[] poss = new Vector3[orbs.Count];
        Vector3[] rots = new Vector3[orbs.Count];

        for (int i = 0; i < orbs.Count; i++)
        {
            poss[i] = orbs[i].Position;
            rots[i] = orbs[i].Rotation;
        }

        trns.Add(_id, poss);
        trns.Add(_id, rots);

        return trns;
    }

    public void MoveAllOrbs()
    {
        foreach (Orb_v5 _orb in orbs)
        {
            _orb.Move();
        }
    }

    public override void OnNotify()
    {
        
    }
}
