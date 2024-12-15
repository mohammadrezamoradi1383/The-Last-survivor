using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerOps : MonoBehaviour
{
    [SerializeField] private Model type;
    [SerializeField] private int count;
    [SerializeField] private Button button;
    [SerializeField] private GameObject disable;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private hurtByZombie playerHealth;
    [SerializeField] private Animator healthAnimator;
    [SerializeField] private ParticleSystem explode;
    private void OnEnable()
    {
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
