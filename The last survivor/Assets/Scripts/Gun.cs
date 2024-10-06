using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
  [SerializeField] ParticleSystem particle;
  [SerializeField]  GameObject bullet;
  [SerializeField] Transform bulletTransform;
 
  
  public void ShootBullet()
    {
    Instantiate(particle, bulletTransform);
    Instantiate(bullet, bulletTransform.position, bulletTransform.rotation);
    
  }
}
