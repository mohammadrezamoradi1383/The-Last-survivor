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
    [SerializeField] private int bulletCountR = 6;
    [SerializeField] private int health;
    [SerializeField] private Transform moshMoshi;
    [SerializeField] private GameObject handPlayer;
    [SerializeField] private Death death;
    private bool reload = false;

    private void Update()
    {
        transform.LookAt(moshMoshi);
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

    public void ShootBullet()
    {
        if (reload == false && bulletCountR > 0)
        {
            rightShooting.SetBool("Shooting", true);
            StartCoroutine(FixAnimation(rightShooting));
            rightgun.ShootBullet();
            bulletCountR--;
            playerInfo.ShowRightMagazine(bulletCountR);
        }
    }

    public void Reloading()
    {
        if (bulletCountR != 6)
        {
            reload = true;
            rightShooting.SetBool("Reload", true);
            Invoke(nameof(ReloadRightGun), timeToReload);
            var value = 6;
            bulletCountR = value;
            playerInfo.ShowRightMagazine(bulletCountR);
        }
    }

    public void GameOver()
    {
        death.ShowDeathAnimation();
        handPlayer.SetActive(false);
    }
}