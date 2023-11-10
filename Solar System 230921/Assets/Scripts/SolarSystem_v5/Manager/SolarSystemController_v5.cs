using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SolarSystemController_v5 : MonoBehaviour
{
    private static SolarSystemController_v5 instance;

    private OrbFactory orbFactory;
    private Data_SolarSystem data;

    public Star_v5 star;
    public List<Orb_v5> orbs;

    private bool isReady = false;

    private void Update()
    {
        MoveAllOrbs();
    }

    public void Init()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        isReady = false;

        data = FindObjectOfType<Data_SolarSystem>();
        data.Init();
    }

    public void Set()
    {
        Create(data.OrbDatas);

        List<Transform> trns = new List<Transform>();
        foreach (Orb_v5 orb in orbs)
        {
            trns.Add(orb.transform);
        }
        data.Set(trns);
    }

    //=======================================================================

    public void Create(List<OrbData> datas)
    {
        SetCapacity(datas.Count);
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
        isReady = true;
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

    public void UpdateAllOrb()
    {
        if (!isReady)
        {
            return;
        }
        for (int i = 0; i < orbs.Count; i++)
        {
            // �¾�� �����Ǳ� ���� ȣ������� ������� �ʵ��� �ϱ� ����
            // ������ �����̳ʿ��� eventManager.GetEvent("orbDatas")?.Invoke(); ��
            // �¾�� �������� ���� ȣ����� �ʵ��� �ڵ� ���ľ���
            if (orbs[i] == null)
            {
                break;
            }
            orbs[i].SetData(data.OrbDatas[i]);
            orbs[i].SetFeature(orbFactory.GetPrefab(orbs[i].data.orbType));
            orbs[i].UpdateTrnasform();
            orbs[i].InitMove();
        }
    }

    public void IsReady(bool _ready)
    {
        isReady = _ready;
    }

    public void MoveAllOrbs()
    {
        if (!isReady)
        {
            return;
        }
        foreach (Orb_v5 _orb in orbs)
        {
            _orb.Move();
        }
    }
}
