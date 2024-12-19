using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoshMoshi : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float minZ = -21f;
    [SerializeField] private float maxZ = 40f;
    [SerializeField] private float maxY = 4f;
    [SerializeField] private float minY = 1f;

    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private bool isSwiping;

    private void Update()
    {
#if UNITY_EDITOR
        HandleMouseSwipe(); // برای شبیه‌سازی در ادیتور
#else
        HandleTouchSwipe(); // برای موبایل
#endif
    }

    private void HandleTouchSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    isSwiping = true;
                    break;

                case TouchPhase.Moved:
                    if (isSwiping)
                    {
                        currentTouchPosition = touch.position;
                        Vector2 swipeDelta = currentTouchPosition - startTouchPosition;

                        MoveObject(swipeDelta);

                        startTouchPosition = currentTouchPosition;
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isSwiping = false;
                    break;
            }
        }
    }

    private void HandleMouseSwipe()
    {
        if (Input.GetMouseButtonDown(0)) // شروع کلیک
        {
            startTouchPosition = Input.mousePosition;
            isSwiping = true;
        }
        else if (Input.GetMouseButton(0)) // حرکت موس
        {
            if (isSwiping)
            {
                currentTouchPosition = Input.mousePosition;
                Vector2 swipeDelta = (Vector2)currentTouchPosition - startTouchPosition;

                MoveObject(swipeDelta);

                startTouchPosition = currentTouchPosition;
            }
        }
        else if (Input.GetMouseButtonUp(0)) // پایان کلیک
        {
            isSwiping = false;
        }
    }

    private void MoveObject(Vector2 swipeDelta)
    {
        Vector3 movement = Vector3.back * (swipeDelta.x * speed * Time.deltaTime) +
                           Vector3.up * (swipeDelta.y * speed * Time.deltaTime);

        Vector3 newPosition = transform.position + movement;
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);
        transform.position = newPosition;
    }
}
