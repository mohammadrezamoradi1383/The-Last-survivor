using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTransform : MonoBehaviour
{
    public Transform[] zombieTransform;

    public Transform zTransform()
    {
       var value = Random.Range(0, zombieTransform.Length);
       return zombieTransform[value];
    }
}
