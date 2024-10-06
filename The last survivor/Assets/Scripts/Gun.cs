using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
  [SerializeField] ParticleSystem particle;
  [SerializeField] Transform bulletTransform;

  public void ShootBullet()
  {
    Instantiate(particle, bulletTransform);
  }
}
