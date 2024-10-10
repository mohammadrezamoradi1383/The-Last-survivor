using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CreatZombie : MonoBehaviour
{
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private Transform[] zombiePosition;
    [SerializeField] private float timeToCreat;

    private void Start()
    {
        StartCoroutine(ZombieMaker());
    }

    IEnumerator ZombieMaker()
    {
        while (true)
        {
            int value = Random.Range(0, zombiePosition.Length);
            Instantiate(zombiePrefab, zombiePosition[value].transform);
            yield return new WaitForSeconds(timeToCreat);
        }
    }
}
