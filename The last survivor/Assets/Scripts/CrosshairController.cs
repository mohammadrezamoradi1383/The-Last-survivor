using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    public GameObject targetObject; 

    private RectTransform crosshairRectTransform;
    [SerializeField] private float xValue;
    [SerializeField] private float yValue;

    void Start()
    {
        crosshairRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetObject.transform.position);
        crosshairRectTransform.position = new Vector2(screenPosition.x + xValue, screenPosition.y+yValue);
    }
}