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
   [SerializeField] private LoadTheGame LoadTheGame;
   [SerializeField] private AudioSource playerInfoAudioSource;
   [SerializeField] private AudioClip damage1;
   [SerializeField] private AudioClip damage2;
   [SerializeField] private int killedCount;
   [SerializeField] int bestScore;
   [SerializeField] private TextMeshProUGUI bestScoreText;
   [SerializeField] private TextMeshProUGUI yourScoreText;
   [SerializeField] private GameObject reloadPanel;
   private void Update()
   {
      yourScoreText.text = $"YourScore {killedCount.ToString()}";
      if (PlayerPrefs.HasKey("BestScore"))
      {
         if (killedCount>PlayerPrefs.GetInt("BestScore"))
         {
            PlayerPrefs.SetInt("BestScore" , killedCount);
            bestScoreText.text =$"bestScore {PlayerPrefs.GetInt("BestScore").ToString()}";
         }
      }
      else
      {
         bestScoreText.text =$"bestScore {killedCount.ToString()}";
      }
   }

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
      Application.targetFrameRate = 60;
      rightGunMagazine.text ="6";
      if (PlayerPrefs.HasKey("BestScore"))
      {
         bestScoreText.text =$"bestScore {PlayerPrefs.GetInt("BestScore").ToString()}";
      }
      else
      {
         bestScoreText.text = "bestScore 0";
      }
   }
   
   public void ShowRightMagazine(int bullet )
   {
      rightGunMagazine.text = $"{bullet}";
   }

   public void ZombieKilled()
   {
      killedCount++;
   }

   public void ShowReloadPanel(bool value)
   {
      reloadPanel.SetActive(value);
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
            playerInfoAudioSource.clip = damage1;
            playerInfoAudioSource.Play();
            color.a = 0.6f;
            BloodSplash.color = color;     
            return;
         case 0:
            color.a = 1;
            BloodSplash.color = color; 
            playerInfoAudioSource.clip = damage2;
            playerInfoAudioSource.Play();
            blackImage.SetActive(true);
            CreatZombie.StopCoroutine();
            LoadTheGame.ShowScore(killedCount);
            player.GameOver();
            return;
      }
   }
   
}
