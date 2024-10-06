using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float destroyTime;
    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime* speed);
        Destroy(gameObject , destroyTime);
    }
}
