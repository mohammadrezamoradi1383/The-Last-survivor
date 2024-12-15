using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurtByZombie : MonoBehaviour
{
    private int health=3;
    [SerializeField] private PlayerInfo playerInfo;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="slap")
        {
            health--;
            playerInfo.BloodSplashHandler(health);
        }
    }

    public void Revive()
    {
        health = 3;
        playerInfo.BloodSplashHandler(health);
    }
}
