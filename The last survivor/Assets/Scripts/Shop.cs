using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button healthBoxShopItem;
    [SerializeField] private GameObject notEnoughMedalFoeBomb;
    [SerializeField] private Button bombBoxShopItem;
    [SerializeField] private GameObject notEnoughMedalFoeHealth;
    [SerializeField] private PowerOps healthPowerOps;
    [SerializeField] private PowerOps bombPowerOps;
    [SerializeField] private PlayerInfo playerInfo;

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
        
    }

    private void BombShopButton()
    {
        
    }

    private void NewGun()
    {
      //todo  
    }
    private void SetActiveShop()
    {
        gameObject.SetActive(false);
    }
}
