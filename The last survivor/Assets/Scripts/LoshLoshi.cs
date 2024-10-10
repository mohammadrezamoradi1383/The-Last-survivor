using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoshLoshi : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="slap")
        {
            Debug.Log("Ahhhhhhhhhhhhhh");
        }
    }
}
