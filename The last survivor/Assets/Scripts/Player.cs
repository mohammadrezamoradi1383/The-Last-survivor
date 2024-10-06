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
  private bool reload = false;

  private void Update()
  {
    if (Input.GetMouseButtonDown(0) && reload==false)
    {
     leftShooting.SetBool("Shooting", true);
     StartCoroutine(FixAnimation(leftShooting));
     leftGun.ShootBullet();
    }

    if (Input.GetMouseButtonDown(1)&& reload==false)
    {
      rightShooting.SetBool("Shooting", true);
      StartCoroutine(FixAnimation(rightShooting));
      rightgun.ShootBullet();
    }

    if (Input.GetKeyDown(KeyCode.R))
    {
      reload = true;
      StartCoroutine(Reloading());
    }
  }

  IEnumerator FixAnimation(Animator animator)
  {
      yield return new WaitForSeconds(0.2f);
      animator.SetBool("Shooting", false);
  }

  IEnumerator Reloading()
  {
     rightShooting.SetBool("Reload", true);
      yield return new WaitForSeconds(2f);
    rightShooting.SetBool("Reload", false);
     leftShooting.SetBool("Reload", true);
     yield return new WaitForSeconds(2f);
    leftShooting.SetBool("Reload", false);
    reload = false;
  }
}
