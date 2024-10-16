using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadTheGame : MonoBehaviour
{
   [SerializeField] private Button loadButton;
   [SerializeField] private TextMeshProUGUI ZombieKilledText; 
   private void OnEnable()
   {
      loadButton.onClick.AddListener(LoadGame);
   }

   private void OnDisable()
   {
      loadButton.onClick.RemoveListener(LoadGame);
   }
   
   private void LoadGame()
   {
      SceneManager.LoadScene("SampleScene");
   }

   public void ShowScore(int num)
   {
      ZombieKilledText.text = $"{num} Killed";
      if (PlayerPrefs.HasKey("BestScore"))
      {
         var value = PlayerPrefs.GetInt("BestScore");
         if (num>value)
         {
            PlayerPrefs.SetInt("BestScore", num);
            PlayerPrefs.Save();
         }
      }
      else
      {
         
         PlayerPrefs.SetInt("BestScore", num);
         PlayerPrefs.Save();
      }
   }
   
}
