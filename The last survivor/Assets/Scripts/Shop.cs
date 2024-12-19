using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private Button gun2Item;
    [SerializeField] private Button gun3Item;
    [SerializeField] private GameObject notEnoughMedalForGun2;
    [SerializeField] private GameObject notEnoughMedalForGun3;
    [SerializeField] private Button gun2ShopItemButton;
    [SerializeField] private Button gun3ShopItemButton;
    [SerializeField] private GameObject starGun2;
    [SerializeField] private GameObject starGun3;
    [SerializeField] private GameObject priceHolderGun2;
    [SerializeField] private GameObject priceHolderGun3;
    [SerializeField] private Player player;
    [SerializeField] private bool haveGun2;
    [SerializeField] private bool haveGun3;
    [SerializeField] private Button revolverGun;
    [SerializeField] private GameObject revolverShopItem;
    [SerializeField] private GameObject pistolShopItem;
    
    private int healthPrice = 5;
    private int bombPrice = 6;
    private int gun2Price = 2;
    private int gun3Price = 2;

    private void Start()
    {
       haveGun2 = LoadBool("Gun2");
       if (haveGun2)
       {
           starGun2.SetActive(true);
           priceHolderGun2.SetActive(false); 
       }

       haveGun3 = LoadBool("Gun3");
       if (haveGun3)
       {
           starGun3.SetActive(true);
           priceHolderGun3.SetActive(false); 
       }
    }

    private void Update()
    {
        var playerMedals = PlayerPrefs.GetInt("Medal");
        notEnoughMedalForHealth.SetActive(playerMedals < healthPrice);
        notEnoughMedalForBomb.SetActive(playerMedals < bombPrice);
        notEnoughMedalForGun2.SetActive(playerMedals < gun2Price);
        notEnoughMedalForGun3.SetActive(playerMedals < gun3Price);
        if (haveGun2) notEnoughMedalForGun2.SetActive(false);
        if (haveGun3) notEnoughMedalForGun3.SetActive(false);
    }

    private void OnEnable()
    {
        closeButton.onClick.AddListener(CloseShop);
        healthBoxShopItem.onClick.AddListener(HealthShopButton);
        bombBoxShopItem.onClick.AddListener(BombShopButton);
        gun2ShopItemButton.onClick.AddListener(Gun2);
        gun3ShopItemButton.onClick.AddListener(Gun3);
        revolverGun.onClick.AddListener(Revolver);
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveListener(CloseShop);
        healthBoxShopItem.onClick.RemoveListener(HealthShopButton);
        bombBoxShopItem.onClick.RemoveListener(BombShopButton);
        gun2ShopItemButton.onClick.RemoveListener(Gun2);
        gun3ShopItemButton.onClick.RemoveListener(Gun3);
        revolverGun.onClick.RemoveListener(Revolver);
    }

    private void Revolver()
    {
        starGun2.SetActive(false);
        player.gun1GameObject.SetActive(true);
        player.gun3GameObject.SetActive(false);
        player.gun2GameObject.SetActive(false);
        player.gunType = GunType.Gun1;
        pistolShopItem.SetActive(true);
        starGun2.SetActive(true);
        revolverShopItem.SetActive(false);
        playerInfo.rightGunMagazine.text = "6";
        player.bulletCountR = 6;
        playerInfo.ShowRightMagazine(6);
        playerInfo.reloadPanel.SetActive(false);
        playerInfo.smgShootButton.SetActive(false);
        playerInfo.smgReloadButton.SetActive(false);
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

    private void Gun2()
    {
        switch (haveGun2)
        {
            case true:
                starGun2.SetActive(true);
                priceHolderGun2.SetActive(false); 
                player.gun1GameObject.SetActive(false);
                player.gun3GameObject.SetActive(false);
                player.gun2GameObject.SetActive(true);
                player.gunType = GunType.Gun2;
                notEnoughMedalForGun2.SetActive(false);
                pistolShopItem.SetActive(false);
                revolverShopItem.SetActive(true);
                playerInfo.rightGunMagazine.text = "12";
                player.bulletCountR = 12;
                playerInfo.ShowRightMagazine(12);
                playerInfo.reloadPanel.SetActive(false);
                playerInfo.smgShootButton.SetActive(false);
                playerInfo.smgReloadButton.SetActive(false);
                break;
            case false:
                var playerMedals = PlayerPrefs.GetInt("Medal");
                if (playerMedals>=gun2Price)
                {
                    starGun2.SetActive(true);
                    priceHolderGun2.SetActive(false); 
                    player.gun1GameObject.SetActive(false);
                    player.gun3GameObject.SetActive(false);
                    player.gun2GameObject.SetActive(true);
                    player.gunType = GunType.Gun2;
                    haveGun2 = true;
                    playerMedals -= gun2Price;
                    PlayerPrefs.SetInt("Medal", playerMedals);
                    SaveBool("Gun2", haveGun2);
                    playerInfo.rightGunMagazine.text = "12";
                    player.bulletCountR = 12;
                    playerInfo.ShowRightMagazine(12);
                    playerInfo.reloadPanel.SetActive(false);
                    playerInfo.smgShootButton.SetActive(false);
                    playerInfo.smgReloadButton.SetActive(false);
                }
                break;
        }
    }
    private void Gun3()
    {
        var playerMedals = PlayerPrefs.GetInt("Medal");
        switch (haveGun3)
        {
            case true:
                starGun3.SetActive(true);
                priceHolderGun3.SetActive(false);
                player.gun1GameObject.SetActive(false);
                player.gun3GameObject.SetActive(true);
                player.gun2GameObject.SetActive(false);
                player.gunType = GunType.Gun3;
                notEnoughMedalForGun2.SetActive(false);
                playerInfo.rightGunMagazine.text = "20";
                playerInfo.ShowRightMagazine(20);
                playerInfo.reloadPanel.SetActive(false);
                playerInfo.smgShootButton.SetActive(true);
                playerInfo.smgReloadButton.SetActive(true);
                break;
            case false:
                if (playerMedals>=gun3Price)
                {
                    starGun3.SetActive(true);
                    priceHolderGun3.SetActive(false);
                    player.gun1GameObject.SetActive(false);
                    player.gun3GameObject.SetActive(true);
                    player.gun2GameObject.SetActive(false);
                    player.gunType = GunType.Gun3;
                    haveGun3 = true;
                    playerMedals -= gun3Price;
                    PlayerPrefs.SetInt("Medal", playerMedals);
                    SaveBool("Gun3", haveGun3);
                    playerInfo.rightGunMagazine.text = "20";
                    playerInfo.ShowRightMagazine(20);
                    playerInfo.reloadPanel.SetActive(false);
                    playerInfo.smgShootButton.SetActive(true);
                    playerInfo.smgReloadButton.SetActive(true);
                }
                break;
        }
    }
    private void SaveBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0); 
        PlayerPrefs.Save(); 
    }


    private bool LoadBool(string key, bool defaultValue = false)
    {
        int defaultInt = defaultValue ? 1 : 0; 
        return PlayerPrefs.GetInt(key, defaultInt) == 1; 
    }
    
}
