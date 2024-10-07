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
  [SerializeField] private float timeToReload;
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
      rightShooting.SetBool("Reload", true);
      Invoke(nameof(ReloadRightGun), timeToReload);
     
    }

    if (Input.GetKeyDown(KeyCode.E))
    {
      reload = true;
      leftShooting.SetBool("Reload", true);
      Invoke(nameof(ReloadingLeftGun), timeToReload);
    }
  }

  IEnumerator FixAnimation(Animator animator)
  {
      yield return new WaitForSeconds(0.2f);
      animator.SetBool("Shooting", false);
  }
  

  private void ReloadingLeftGun()
  {
     leftShooting.SetBool("Reload", false);
     reload = false;
  }

  private void ReloadRightGun()
  {
    rightShooting.SetBool("Reload", false);
    reload = false;
  }
    
}
