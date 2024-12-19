using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private int health;
    [SerializeField] private Transform moshMoshi;
    [SerializeField] private GameObject handPlayer;
    [SerializeField] private Death death;
    [SerializeField] private Gun gun1;
    [SerializeField] private Gun gun2;
    [SerializeField] private Gun gun3;
    public float playReloading;
    public AudioClip reloadingAudioClip;
    public float timeToReload;
    public int bulletCountR = 6;
    public AudioClip shootingAudioClip;
    public AudioSource playeraAudioSource;
    public AudioClip reloadAudioClip;
    public Animator rightShooting;
    public GameObject gun1GameObject;
    public GameObject gun2GameObject;
    public GameObject gun3GameObject;
    public GunType gunType;
    public bool reload = false;

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
            playerInfo.shootingButton.interactable = false;
            playeraAudioSource.clip = shootingAudioClip;
            playeraAudioSource.Play();
            rightShooting.SetBool("Shooting", true);
            StartCoroutine(FixAnimation(rightShooting));
            switch (gunType)
            {
                case GunType.Gun1:
                    playerInfo.smgShootButton.SetActive(false);
                    playerInfo.smgShootButton.SetActive(false);
                    gun1.ShootBullet();
                    break;
                case GunType.Gun2:
                    playerInfo.smgShootButton.SetActive(false);
                    playerInfo.smgShootButton.SetActive(false);
                    gun2.ShootBullet();
                    break;
                case GunType.Gun3:
                    break;
            }

            bulletCountR--;
            playerInfo.ShowRightMagazine(bulletCountR);
            Invoke(nameof(SetShooting), 0.7f);
        }
        else
        {
            if (bulletCountR == 0)
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
        playerInfo.DontShowAnyThing();
    }
    public void SetShooting()
    {
       playerInfo.shootingButton.interactable = true;
    }
}
public enum GunType
{
    Gun1,
    Gun2,
    Gun3
}

