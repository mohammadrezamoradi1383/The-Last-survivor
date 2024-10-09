using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField] private Animator rightShooting;
  [SerializeField] private Gun rightgun;
  [SerializeField] private float timeToReload;
  [SerializeField] private PlayerInfo playerInfo;
  [SerializeField] private int bulletCountR=6;
  [SerializeField] private int magazineCountR=30;
  [SerializeField] private int health;
  [SerializeField] private Transform moshMoshi;
  private bool reload = false;

  private void Update()
  {
    transform.LookAt(moshMoshi);
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
    
  }

  IEnumerator FixAnimation(Animator animator)
  {
      yield return new WaitForSeconds(0.2f);
      animator.SetBool("Shooting", false);
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
