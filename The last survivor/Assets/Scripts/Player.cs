using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField] private Animator leftShooting;
  [SerializeField] private Animator rightShooting;
  [SerializeField] private Gun rightgun;
  [SerializeField] private Gun leftGun;

  private void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
     leftShooting.SetBool("Shooting", true);
     StartCoroutine(FixAnimation(leftShooting));
     leftGun.ShootBullet();
    }

    if (Input.GetMouseButtonDown(1))
    {
      rightShooting.SetBool("Shooting", true);
      StartCoroutine(FixAnimation(rightShooting));
      rightgun.ShootBullet();
    }
  }

  IEnumerator FixAnimation(Animator animator)
  {
      yield return new WaitForSeconds(0.2f);
      animator.SetBool("Shooting", false);
  }
}
