using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoshMoshi : MonoBehaviour
{
    [SerializeField] private VariableJoystick variableJoystick;
    [SerializeField] private float speed;
    [SerializeField] private float minZ = -21f;
    [SerializeField] private float maxZ = 40f;
    [SerializeField] private float maxY = 4f;
    [SerializeField] private float minY = 1f;

    private void Update()
    {
        Vector3 movement = Vector3.up * variableJoystick.Vertical + Vector3.back * variableJoystick.Horizontal;
        Vector3 newPosition = transform.position + movement * (speed * Time.deltaTime);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);
        transform.position = newPosition;
    }
}
