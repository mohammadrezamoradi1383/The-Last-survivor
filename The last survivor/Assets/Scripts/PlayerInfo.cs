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
   
}
