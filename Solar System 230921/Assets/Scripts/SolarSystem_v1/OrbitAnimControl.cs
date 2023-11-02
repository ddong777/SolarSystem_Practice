using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitAnimControl : MonoBehaviour
{
    private Animation animation;
    public float orbitSpeed;

    private void Start()
    {
        animation = GetComponent<Animation>();

        // 따로 공전 속도를 정하지 않으면 랜덤값
        if (orbitSpeed == 0)
            orbitSpeed = Random.Range(0.01f, 0.2f);

        animation[animation.clip.name].speed = orbitSpeed;
    }
}
