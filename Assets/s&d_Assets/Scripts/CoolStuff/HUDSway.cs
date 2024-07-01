using UnityEngine;
using UnityEngine.UI;

public class HUDSway : MonoBehaviour
{
    public RectTransform hudRectTransform;
    public float swayAmount = 10f;
    public float maxSwayAmount = 20f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = hudRectTransform.anchoredPosition;
    }

    void Update()
    {
        float movementX = Input.GetAxis("Horizontal");
        float movementY = Input.GetAxis("Vertical");

        float swayX = Mathf.Clamp(-movementX * swayAmount, -maxSwayAmount, maxSwayAmount);
        float swayY = Mathf.Clamp(-movementY * swayAmount, -maxSwayAmount, maxSwayAmount);

        Vector3 sway = new Vector3(swayX, swayY, 0f);
        Vector3 finalPosition = initialPosition + sway;

        hudRectTransform.anchoredPosition = Vector3.Lerp(hudRectTransform.anchoredPosition, finalPosition, Time.deltaTime * 5f);
    }
}
