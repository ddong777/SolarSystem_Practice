using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAnimControl : MonoBehaviour
{
    public float spinSpeed = 1f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("spinSpeed", spinSpeed);
    }
}
