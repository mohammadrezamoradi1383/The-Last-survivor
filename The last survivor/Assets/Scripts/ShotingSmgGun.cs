using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShotingSmgGun : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isButtonHeld = false; 
    private bool isCoroutineRunning = false; 
    [SerializeField] private Gun SmgGun;
    [SerializeField] private Player player;
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private int smgMagazine;
    void Update()
    {
        if (isButtonHeld && !isCoroutineRunning) 
        {
            if (smgMagazine>0)
            {
                StartCoroutine(ShootSmg());
            }
            else
            {
                if (smgMagazine==0)
                {
                    if ((player.gunType == GunType.Gun3))
                    {
                     playerInfo.ShowReloadPanel(true);
                    }
                    player.playeraAudioSource.clip =player.reloadAudioClip;
                    player.playeraAudioSource.Play();
                }
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonHeld = true; 
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonHeld = false; 
    }

    IEnumerator FixAnimation(Animator animator)
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("Shooting", false);
    }

    IEnumerator ShootSmg()
    {
        isCoroutineRunning = true; 
        SmgGun.ShootBullet();
        player.playeraAudioSource.clip = player.shootingAudioClip;
        player.playeraAudioSource.Play();
        player.rightShooting.SetBool("Shooting", true);
        StartCoroutine(FixAnimation(player.rightShooting));
        smgMagazine--;
        playerInfo.ShowRightMagazine(smgMagazine);
        yield return new WaitForSeconds(0.2f); 
        isCoroutineRunning = false; 
    }
    public void SmgReloading()
    {
        if (smgMagazine != 20)
        {
            if ((player.gunType == GunType.Gun3))
            {
                playerInfo.ShowReloadPanel(false);
            }
            player.reload = true;
            player.rightShooting.SetBool("Reload", true);
            Invoke(nameof(ReloadRightGun),player.timeToReload);
            var value = 20;
            smgMagazine = value;
            player.playeraAudioSource.clip =player.reloadingAudioClip;
            Invoke(nameof(PlayReloading),player.playReloading);
        }
    }
    private void ReloadRightGun()
    {
       player.rightShooting.SetBool("Reload", false);
       player.reload = false;
    }
    private void PlayReloading()
    {
        player.playeraAudioSource.Play();
        playerInfo.ShowRightMagazine(smgMagazine);
    }
}