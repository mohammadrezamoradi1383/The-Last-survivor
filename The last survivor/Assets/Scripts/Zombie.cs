using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Zombie : MonoBehaviour
{
    [SerializeField] private Transform StartMoving;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float duration;
    [SerializeField] private Animator zombieAnimator;
    [SerializeField] private RandomTransform ZombieTransform;
    [SerializeField] private PlayerInfo playerScore;
    [SerializeField] private float timeToRotate;
    [SerializeField] private int health;
    [SerializeField] private ParticleSystem zombieBlood;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip deadSfx;
    [SerializeField] private AudioClip damageSfx;
    
    
    private float elapsedTime;
    private bool waiting;

    private enum State
    {
        MovingToStart,
        MovingToPlayer
    }

    private State currentState = State.MovingToStart;

    private void Start()
    {
        playerScore = FindObjectOfType<PlayerInfo>();
        zombieAnimator.SetBool("Walk", true);
        ZombieTransform = FindObjectOfType<RandomTransform>();
        StartMoving = ZombieTransform.ZombieTransformForMoving(transform);
        playerTransform = ZombieTransform.PlayerTransformForAttack();
        audioSource.clip = damageSfx;
    }

    private void Update()
    {
        if (health > 0)
        {
            if (waiting == false)
            {
                elapsedTime += Time.deltaTime;

                switch (currentState)
                {
                    case State.MovingToStart:
                        if (transform.position.z > 0)
                        {
                            float currentYRotation = transform.rotation.eulerAngles.y;


                            if (currentYRotation > 220f || currentYRotation < -90f)
                            {
                                if (currentYRotation > -140f || currentYRotation < 220f)
                                {
                                    timeToRotate = 10;
                                    transform.Rotate(Vector3.down * timeToRotate * Time.deltaTime);
                                }
                            }
                        }

                        if (transform.position.z < 0)
                        {
                            if (transform.rotation.y >= -30f)
                            {
                                transform.Rotate(Vector3.up * timeToRotate * Time.deltaTime);
                            }
                        }

                        MoveTowards(StartMoving.position);
                        if (Vector3.Distance(transform.position, StartMoving.position) < 0.1f)
                        {
                            currentState = State.MovingToPlayer;
                            elapsedTime = 0f;
                            if (transform.position.z < 0)
                            {
                                transform.eulerAngles = new Vector3(transform.rotation.x, -90, transform.rotation.z);
                            }

                            if (transform.position.z > 0)
                            {
                                transform.eulerAngles = new Vector3(transform.rotation.x, -90, transform.rotation.z);
                            }
                        }

                        break;

                    case State.MovingToPlayer:
                        MoveTowards(playerTransform.position);
                        if (Vector3.Distance(transform.position, playerTransform.position) < 0.1f)
                        {
                            zombieAnimator.SetBool("Attack", true);
                        }

                        break;
                }
            }
        }
        else
        {
            zombieAnimator.SetBool("Death", true);
            Destroy(gameObject, 2f);
        }
    }
    
    private void MoveTowards(Vector3 targetPosition)
    {
        float time = elapsedTime / duration;
        transform.position = Vector3.Lerp(transform.position, targetPosition, time * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            ParticleSystem bloodInstance = Instantiate(zombieBlood, other.transform.position, other.transform.rotation);
            bloodInstance.Play();
            Destroy(bloodInstance.gameObject, bloodInstance.main.duration + bloodInstance.main.startLifetime.constantMax);
            StartCoroutine(waitZombie());
        }
    }

    IEnumerator waitZombie()
    {
        waiting = true;
        zombieAnimator.SetBool("Damage", true);
        audioSource.Play();
        health--;
        if (health==0)
        {
            audioSource.clip = deadSfx;
            audioSource.Play();
            playerScore.ZombieKilled();
        }
        yield return new WaitForSeconds(0.4f);
        zombieAnimator.SetBool("Damage", false);
        waiting = false;
    }

    public void HeadShot()
    {
        health -= 3;
        playerScore.ZombieKilled();
    }
}