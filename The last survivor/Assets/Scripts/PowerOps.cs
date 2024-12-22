using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerOps : MonoBehaviour
{
    [SerializeField] private Model type;
    //[SerializeField] private int count;
    [SerializeField] private Button button;
    [SerializeField] private GameObject disable;
    [SerializeField] private GameObject indicator;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private hurtByZombie playerHealth;
    [SerializeField] private Animator healthAnimator;
    [SerializeField] private ParticleSystem explode;

    private void Update()
    {
        switch (type)
        {
            case Model.Bomb:
                var bombValue = PlayerPrefs.GetInt("Bomb");
                if (bombValue==0)
                {
                    disable.SetActive(true);
                    indicator.SetActive(false);
                    button.interactable = false;

                }
                else
                {
                    disable.SetActive(false);
                    indicator.SetActive(true);
                    button.interactable = true;
                    countText.text = PlayerPrefs.GetInt("Bomb").ToString();
                }
                break;
            case Model.Health:
                var healthValue = PlayerPrefs.GetInt("Health");
                if (healthValue==0)
                {
                    disable.SetActive(true);
                    indicator.SetActive(false);
                    button.interactable = false;
                }
                else
                {
                    disable.SetActive(false);
                    indicator.SetActive(true);
                    button.interactable = true;
                    countText.text = PlayerPrefs.GetInt("Health").ToString();
                }
                break;
        }
    }

    public void IncreaseCount()
    {
        switch (type)
        {
            case Model.Bomb:
            var bombCount = PlayerPrefs.GetInt("Bomb");
            bombCount++;
            PlayerPrefs.SetInt("Bomb" , bombCount); 
            countText.text = PlayerPrefs.GetInt("Bomb").ToString();
            PlayerPrefs.Save();
            break;
            case Model.Health:
                var HealthCount = PlayerPrefs.GetInt("Health");
                HealthCount++;
            PlayerPrefs.SetInt("Health", HealthCount);
            countText.text = PlayerPrefs.GetInt("Health").ToString();
            PlayerPrefs.Save();
            break;
        }

    }
    private void OnEnable()
    {
        switch (type)
        {
            case Model.Health:
                countText.text = PlayerPrefs.GetInt("Health").ToString();
                break;
            case Model.Bomb:
                countText.text = PlayerPrefs.GetInt("Bomb").ToString();
                break;
        }
        button.onClick.AddListener(OnClickPowerOps);
    }

    private void OnDisable()
    {
     button.onClick.RemoveListener(OnClickPowerOps);   
    }

    private void OnClickPowerOps()
    {
        switch (type)
        {
            case Model.Bomb:
                var bombValue = PlayerPrefs.GetInt("Bomb");
                bombValue--;
                PlayerPrefs.SetInt("Bomb" , bombValue);
                PlayerPrefs.Save();
                var zombies = FindObjectsOfType<Zombie>();
                foreach (var t in zombies)
                {
                    t.health = 0;
                    ParticleSystem particleSystem = Instantiate(explode, t.explodeParticleTransform.transform.position, t.explodeParticleTransform.transform.rotation);
                    particleSystem.Play();
                    Destroy(particleSystem.gameObject, particleSystem.main.duration + particleSystem.main.startLifetime.constantMax);
                }
                break;
            case Model.Health:
                var healthValue = PlayerPrefs.GetInt("Health");
                healthValue--;
                PlayerPrefs.SetInt("Health" , healthValue);
                PlayerPrefs.Save();
                playerHealth.Revive();
                healthAnimator.SetBool("ShowHealthSplash", true);
                Invoke(nameof(HideHealthSplash), 3f);
                break;
        }
    }
    public void HideHealthSplash()
    {
        healthAnimator.SetBool("ShowHealthSplash", false);
    }
}

public enum Model
{
    Bomb,
    Health
}
