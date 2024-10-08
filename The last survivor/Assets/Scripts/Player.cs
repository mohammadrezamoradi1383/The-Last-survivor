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
  [SerializeField] private PlayerInfo playerInfo;
  
  [SerializeField] private int bulletCountL=6;
  [SerializeField] private int magazineCountL=30;
  
  [SerializeField] private int bulletCountR=6;
  [SerializeField] private int magazineCountR=30;

  [SerializeField] private int health;
  private bool reload = false;

  private void Update()
  {
    if (Input.GetMouseButtonDown(0) && reload==false&& bulletCountL>0)
    {
     leftShooting.SetBool("Shooting", true);
     StartCoroutine(FixAnimation(leftShooting));
     leftGun.ShootBullet();
     bulletCountL--;
     playerInfo.ShowLeftMagazine(bulletCountL , magazineCountL);
    }

    if (Input.GetMouseButtonDown(1)&& reload==false&& bulletCountR>0)
    {
      rightShooting.SetBool("Shooting", true);
      StartCoroutine(FixAnimation(rightShooting));
      rightgun.ShootBullet();
      bulletCountR--;
      playerInfo.ShowRightMagazine(bulletCountR , magazineCountR);
    }

    if (Input.GetKeyDown(KeyCode.R)&& magazineCountR>0&& bulletCountR!=6)
    {
      reload = true;
      rightShooting.SetBool("Reload", true);
      Invoke(nameof(ReloadRightGun), timeToReload);
      var value = 6;
      if(magazineCountR<value) value = magazineCountR;
       magazineCountR -= value;
       bulletCountR = value;
      playerInfo.ShowRightMagazine(bulletCountR, magazineCountR);
    }

    if (Input.GetKeyDown(KeyCode.E)&& magazineCountL>0&& bulletCountL!=6)
    {
      reload = true;
      leftShooting.SetBool("Reload", true);
      Invoke(nameof(ReloadingLeftGun), timeToReload);
      var value = 6;
      if (magazineCountL < value) value = magazineCountL;
      magazineCountL -= value;
      bulletCountL = value;
      playerInfo.ShowLeftMagazine(bulletCountL , magazineCountL);
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

  public void gettingHurt()
  {
    health--;
    playerInfo.ShowHealth(health);
  }
}
