using UnityEngine;

public abstract class Orb_v5 : MonoBehaviour, ISpinable, IOrbitable
{
    public OrbData data;

    [Space(10)]
    [Header("Transforms")]
    public Transform featureTrn;
    public string featureTrnName = "Feature";

#if UNITY_EDITOR
    [Rename("자전축")]
#endif
    public Transform spinAxis;
#if UNITY_EDITOR
    [Rename("공전체")]
#endif
    public Transform orbitTrn;

    public Vector3 Position
    {
        get => transform.localPosition;
        set => transform.localPosition = value;
    }
    public Vector3 Rotation
    {
        get => transform.localRotation.eulerAngles;
        set => transform.localRotation = Quaternion.Euler(value);
    }

    /// <summary> ====================================================================
    /// 천체 설정
    /// </summary>====================================================================

    public void SetFeature(GameObject _prefab)
    {
        if (featureTrn == null)
        {
            featureTrn = transform.GetChild(0).Find(featureTrnName);
        }

        data.orbPrefab = _prefab;

        // 가지고 있는 prefab 있을 경우 모두 날리고 다시 생성
        for (int i = 0; i < featureTrn.childCount; i++)
        {
            Destroy(featureTrn.GetChild(i).gameObject);
        }

        Instantiate(data.orbPrefab, featureTrn);
        featureTrn.localScale = new Vector3(data.orbSize, data.orbSize, data.orbSize);
    }

    public void SetData(OrbData _data)
    {
        data = _data;
    }

    public void SetTransform(Vector3 pos, Vector3 rot)
    {
        transform.localPosition = pos;
        transform.localRotation = Quaternion.Euler(rot);
    }

    /// <summary>====================================================================
    /// 자전
    /// </summary>====================================================================
    public virtual void InitSpin()
    {
        if (spinAxis == null)
        {
            spinAxis = transform.GetChild(0);
        }

        // 따로 자전 속도를 정하지 않으면 랜덤값
        if (data.spinSpeed == 0)
        {
            data.spinSpeed = Random.Range(10f, 100f);
        }
    }
    public virtual void Spin()
    {
        if (spinAxis == null)
        {
            InitSpin();
            return;
        }

        if (data.spinDir == MoveDir.rightDir)
            spinAxis.Rotate(data.spinSpeed * Time.deltaTime * Vector3.up);
        else
            spinAxis.Rotate(-data.spinSpeed * Time.deltaTime * Vector3.up);
    }

    /// <summary>====================================================================
    /// 공전
    /// </summary>====================================================================
    public virtual void InitOrbit()
    {
        if (orbitTrn == null)
        {
            orbitTrn = transform;
        }
    }

    public virtual void Orbit()
    {
        if (orbitTrn == null)
        {
            InitOrbit();
            return;
        }
    }

    /// <summary>
    /// 운동
    /// </summary>
    public abstract void InitMove();
    public abstract void Move();
}
