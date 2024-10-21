using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShot : MonoBehaviour
{
 [SerializeField] private Zombie zombie;
 [SerializeField] private AudioClip headShotSoundEffect;
 [SerializeField] private AudioSource AudioSource;
 
 private void Start()
 {
     AudioSource.clip = headShotSoundEffect;
 }

 private void OnCollisionEnter(Collision other)
 {
     throw new NotImplementedException();
 }

 private void OnTriggerEnter(Collider other)
 {
  if (other.gameObject.tag=="bullet")
  {
    zombie.HeadShot();
    AudioSource.Play();
  }
 }
}
