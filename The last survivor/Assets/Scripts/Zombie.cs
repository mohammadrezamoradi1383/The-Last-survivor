using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
  [SerializeField] private Animator zombieAnimator;
  [SerializeField] private float duration=20;
  [SerializeField] private RandomTransform RandomTransform;
  private Transform playerTransform;
   private float elapsedTime;

  private void Start()
  {
    RandomTransform = FindObjectOfType<RandomTransform>();
    playerTransform =RandomTransform.zTransform();
    zombieAnimator.SetBool("Walk", true);
  }

  private void Update()
  {
    elapsedTime += Time.deltaTime;
    float time = elapsedTime / duration;
    transform.position = Vector3.Lerp(transform.position, playerTransform.position, time * Time.deltaTime);
    if (Vector3.Distance(transform.position, playerTransform.position) < 0.1f)
    {
      zombieAnimator.SetBool("Attack", true);
    } 
  }
}
