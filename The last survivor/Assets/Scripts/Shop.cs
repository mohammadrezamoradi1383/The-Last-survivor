using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button healthBoxShopItem;
    [SerializeField] private GameObject notEnoughMedalForBomb;
    [SerializeField] private Button bombBoxShopItem;
    [SerializeField] private GameObject notEnoughMedalForHealth;
    [SerializeField] private PowerOps healthPowerOps;
    [SerializeField] private PowerOps bombPowerOps;
    [SerializeField] private PlayerInfo playerInfo;

    private int healthPrice = 5;
    private int bombPrice = 6;

    private void Update()
    {
        var playerMedals = PlayerPrefs.GetInt("Medal");
        notEnoughMedalForHealth.SetActive(playerMedals < healthPrice);
        notEnoughMedalForBomb.SetActive(playerMedals < bombPrice);
    }

    private void OnEnable()
    {
        closeButton.onClick.AddListener(CloseShop);
        healthBoxShopItem.onClick.AddListener(HealthShopButton);
        bombBoxShopItem.onClick.AddListener(BombShopButton);
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveListener(CloseShop);
        healthBoxShopItem.onClick.RemoveListener(HealthShopButton);
        bombBoxShopItem.onClick.RemoveListener(BombShopButton);
    }

    private void CloseShop()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void HealthShopButton()
    {
        var playerMedals = PlayerPrefs.GetInt("Medal");
        if (playerMedals>=healthPrice)
        {
            healthPowerOps.IncreaseCount();
            playerMedals -= healthPrice;
            playerInfo.SetMedal(playerMedals);
        }
    }

    private void BombShopButton()
    {
        var playerMedals = PlayerPrefs.GetInt("Medal");
        if (playerMedals>=bombPrice)
        {
            bombPowerOps.IncreaseCount();
            playerMedals -= bombPrice;
            playerInfo.SetMedal(playerMedals);
        }
    }

    private void NewGun()
    {
        //todo  
    }
    
}
