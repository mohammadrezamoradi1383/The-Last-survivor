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
    [SerializeField] private float timeToRotate;
    private float elapsedTime;

    private enum State
    {
        MovingToStart,
        MovingToPlayer
    }

    private State currentState = State.MovingToStart;

    private void Start()
    {
        zombieAnimator.SetBool("Walk", true);
        ZombieTransform = FindObjectOfType<RandomTransform>();
        StartMoving = ZombieTransform.ZombieTransformForMoving(transform);
        playerTransform = ZombieTransform.PlayerTransformForAttack();
    }

    private void Update()
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

    private void MoveTowards(Vector3 targetPosition)
    {
        float time = elapsedTime / duration;
        transform.position = Vector3.Lerp(transform.position, targetPosition, time * Time.deltaTime);
    }
}