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
        // محاسبه حرکت بر اساس ورودی جوی‌استیک
        Vector3 movement = Vector3.up * variableJoystick.Vertical + Vector3.back * variableJoystick.Horizontal;

        // محاسبه موقعیت جدید
        Vector3 newPosition = transform.position + movement * (speed * Time.deltaTime);

        // محدود کردن محور Y
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        // محدود کردن محور Z
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        // اعمال موقعیت محدود شده به آبجکت
        transform.position = newPosition;
    }
}
