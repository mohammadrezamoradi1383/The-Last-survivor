using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
   [SerializeField] private int numberToShowLevelUp;
   [SerializeField] private LevelUpDialog levelUpDialog;
   [SerializeField] private TextMeshProUGUI medalCount;
   [SerializeField] private int medalValue=0;
   [SerializeField] private GameObject bombPowerOps;
   [SerializeField] private GameObject healthPowerOps;
   [SerializeField] private Button medalButton;
   [SerializeField] private GameObject shopDialogGameObject;
   

   public int Medal()
   {
      return medalValue;
   }

   public void SetMedal(int value)
   {
      PlayerPrefs.SetInt("Medal", value);
      medalCount.text = value.ToString();
   }
   private int medalLevel=10;
   private int _iconValue = 0;
   private void Update()
   {
      if (killedCount>=numberToShowLevelUp)
      {
         MedalController();
         levelUpDialog.ShowDialog(_iconValue);
         if (numberToShowLevelUp<=50)
         {
            numberToShowLevelUp += 10;
            CreatZombie.ChangeTimeToCreatZombie();
            _iconValue++;
         }
         else
         {
            levelUpDialog.FinishAnimation();
         }
      }
      
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
      medalButton.onClick.AddListener(ShowShopDialog);
   }

   private void OnDisable()
   {
     reloadingButton.onClick.RemoveListener(player.Reloading);
     shootingButton.onClick.RemoveListener(player.ShootBullet);
     medalButton.onClick.RemoveListener(ShowShopDialog);
   }

   private void ShowShopDialog()
   {
      shopDialogGameObject.SetActive(true);
      Time.timeScale = 0;
   }
   
   private void Start()
   {
      Application.targetFrameRate = 60;
      rightGunMagazine.text ="6";
      bestScoreText.text = PlayerPrefs.HasKey("BestScore") ? $"bestScore {PlayerPrefs.GetInt("BestScore").ToString()}" : "bestScore 0";
      medalCount.text = PlayerPrefs.GetInt("Medal").ToString();
      
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
         case 3:
            color.a = 0f;
            BloodSplash.color = color;
            return;
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

   public void DontShowAnyThing()
   {
      reloadPanel.SetActive(false);
      bombPowerOps.SetActive(false);
      healthPowerOps.SetActive(false);
   }

   private void MedalController()
   {
      
      if (killedCount >= medalLevel)
      {
         if (PlayerPrefs.HasKey("Medal"))
         {
            medalValue = PlayerPrefs.GetInt("Medal");
         }
         medalValue++;
         PlayerPrefs.SetInt("Medal", medalValue); 
         PlayerPrefs.Save(); 
         medalCount.text = medalValue.ToString(); 
         medalLevel += 10; 
      }
   }
}
