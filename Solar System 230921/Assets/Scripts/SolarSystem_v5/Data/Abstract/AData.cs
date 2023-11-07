using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AData : MonoBehaviour 
{
    protected DataContainer data;
    protected Converter converter;

    private void Awake()
    {
        data = FindObjectOfType<DataContainer>();
        converter = new Converter();
    }

    public virtual void Init() { }
}
