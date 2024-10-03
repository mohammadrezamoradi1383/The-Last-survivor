using UnityEngine;

public class RotateWithMouse : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            transform.LookAt(raycastHit.point);

        }
    }
}