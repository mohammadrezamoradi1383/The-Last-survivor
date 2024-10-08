using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
   [SerializeField] TextMeshProUGUI leftGunMagazine;
   [SerializeField] TextMeshProUGUI rightGunMagazine;
   [SerializeField] Slider healthSlider;

   private void Start()
   {
      leftGunMagazine.text = "30-6";
      rightGunMagazine.text ="30-6";
   }

   public void ShowLeftMagazine(int bullet , int magazine)
   {
      leftGunMagazine.text = $"{magazine}_{bullet}";
   }
   public void ShowRightMagazine(int bullet , int magazine)
   {
      rightGunMagazine.text = $"{magazine}_{bullet}";
   }

   public void ShowHealth(int playerHealth)
   {
      healthSlider.value = playerHealth;
   }
}
