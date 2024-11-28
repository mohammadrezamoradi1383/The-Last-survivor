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
    [SerializeField] private AudioSource playeraAudioSource;
    [SerializeField] private AudioClip shootingAudioClip;
    [SerializeField] private AudioClip reloadingAudioClip;
    [SerializeField] private float playReloading;
    [SerializeField] private AudioClip reloadAudioClip;
    private bool reload = false;

    private void Awake()
    {
        Time.timeScale = 1;
    }

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
            playeraAudioSource.clip = shootingAudioClip;
            playeraAudioSource.Play();
            rightShooting.SetBool("Shooting", true);
            StartCoroutine(FixAnimation(rightShooting));
            rightgun.ShootBullet();
            bulletCountR--;
            playerInfo.ShowRightMagazine(bulletCountR);
        }
        else
        {
            if (bulletCountR==0)
            {
                playerInfo.ShowReloadPanel(true);
                playeraAudioSource.clip = reloadAudioClip;
                playeraAudioSource.Play();
            }
        }
    }

    public void Reloading()
    {
        if (bulletCountR != 6)
        {
            playerInfo.ShowReloadPanel(false);
            reload = true;
            rightShooting.SetBool("Reload", true);
            Invoke(nameof(ReloadRightGun), timeToReload);
            var value = 6;
            bulletCountR = value;
            playeraAudioSource.clip = reloadingAudioClip;
            Invoke(nameof(PlayReloading), playReloading);
        }
    }

    private void PlayReloading()
    {
            playeraAudioSource.Play();
            playerInfo.ShowRightMagazine(bulletCountR);
        
    }

    public void GameOver()
    {
        death.ShowDeathAnimation();
        handPlayer.SetActive(false);
    }
}