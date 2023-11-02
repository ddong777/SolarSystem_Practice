using UnityEngine;

public abstract class Orb_v5 : MonoBehaviour, ISpinable, IOrbitable
{
    public OrbData data;

    [Space(10)]
    [Header("Transforms")]
    public Transform featureTrn;
    public string featureTrnName = "Feature";

#if UNITY_EDITOR
    [Rename("������")]
#endif
    public Transform spinAxis;
#if UNITY_EDITOR
    [Rename("����ü")]
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
    /// õü ����
    /// </summary>====================================================================

    public void SetFeature(GameObject _prefab)
    {
        if (featureTrn == null)
        {
            featureTrn = transform.GetChild(0).Find(featureTrnName);
        }

        data.orbPrefab = _prefab;

        // ������ �ִ� prefab ���� ��� ��� ������ �ٽ� ����
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
    /// ����
    /// </summary>====================================================================
    public virtual void InitSpin()
    {
        if (spinAxis == null)
        {
            spinAxis = transform.GetChild(0);
        }

        // ���� ���� �ӵ��� ������ ������ ������
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
    /// ����
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
    /// �
    /// </summary>
    public abstract void InitMove();
    public abstract void Move();
}
