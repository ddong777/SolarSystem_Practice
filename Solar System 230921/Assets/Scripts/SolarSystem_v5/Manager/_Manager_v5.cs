using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Manager_v5 : MonoBehaviour, IObserver
{
    public virtual void OnNotify() { }
    public virtual void OnNotify(string _data) { }
    public virtual void OnNotify(OrbData _data) { }
    public virtual void OnNotify(string[] _datas) { }
    public virtual void OnNotify(List<OrbData> _datas) { }
    public virtual void OnNotify(Vector3 pos, Vector3 rot) { }
}
