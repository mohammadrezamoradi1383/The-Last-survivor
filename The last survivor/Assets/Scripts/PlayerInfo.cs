using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
   [SerializeField] private Player player;
   [SerializeField] TextMeshProUGUI rightGunMagazine;
   [SerializeField] private Button reloadingButton;
   [SerializeField] private Button shootingButton;
   [SerializeField] private Image BloodSplash;
   [SerializeField] private GameObject blackImage;
   [SerializeField] private CreatZombie CreatZombie;

   private void OnEnable()
   {
      reloadingButton.onClick.AddListener(player.Reloading);
      shootingButton.onClick.AddListener(player.ShootBullet);
   }

   private void OnDisable()
   {
     reloadingButton.onClick.RemoveListener(player.Reloading);
     shootingButton.onClick.RemoveListener(player.ShootBullet);
   }

   private void Start()
   {
      rightGunMagazine.text ="6";
   }
   
   public void ShowRightMagazine(int bullet )
   {
      rightGunMagazine.text = $"{bullet}";
   }

   public void BloodSplashHandler(int health)
   {

      Color color = BloodSplash.color;
      switch (health)
      {
         case 2:
            color.a = 0.4f;
            BloodSplash.color = color;
            return;
         case 1:
            color.a = 0.6f;
            BloodSplash.color = color;     
            return;
         case 0:
            color.a = 1;
            BloodSplash.color = color; 
            blackImage.SetActive(true);
            CreatZombie.StopCoroutine();
            player.GameOver();
            return;
      }
   }
   
}
