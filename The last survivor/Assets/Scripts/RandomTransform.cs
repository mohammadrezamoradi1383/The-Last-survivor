using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RandomTransform : MonoBehaviour
{
    public Transform[] playerTransformForZombie;
    public Transform[] transformForStartMovingZombie;
    public Transform PlayerTransformForAttack()
    {
       var value = Random.Range(0, playerTransformForZombie.Length);
       return playerTransformForZombie[value];
    }

    public Transform ZombieTransformForMoving(Transform zombieTransform)
    {
        if (zombieTransform.position.z>0)
        {
           return transformForStartMovingZombie[1];
        }
        else
        {
            return transformForStartMovingZombie[0];
        }
    }
}
