using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
   [SerializeField] TextMeshProUGUI rightGunMagazine;
   

   private void Start()
   {
      rightGunMagazine.text ="30-6";
   }
   
   public void ShowRightMagazine(int bullet , int magazine)
   {
      rightGunMagazine.text = $"{magazine}_{bullet}";
   }

   public void ShowHealth(int playerHealth)
   {
     
   }
}
