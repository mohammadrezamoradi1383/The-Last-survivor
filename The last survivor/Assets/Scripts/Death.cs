using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
  [SerializeField] private Animator deathAnimation;

  public void ShowDeathAnimation()
  {
    deathAnimation.SetBool("Death", true);
  }
}
