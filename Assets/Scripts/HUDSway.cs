using UnityEngine;
using UnityEngine.UI;

public class HUDSway : MonoBehaviour
{
    public RectTransform hudRectTransform;
    public float swayAmount = 10f; // How much the HUD moves
    public float maxSwayAmount = 20f; // Maximum sway amount

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = hudRectTransform.anchoredPosition;
    }

    void Update()
    {
        float movementX = Input.GetAxis("Horizontal");
        float movementY = Input.GetAxis("Vertical");

        // Calculate the sway based on player's movement
        float swayX = Mathf.Clamp(-movementX * swayAmount, -maxSwayAmount, maxSwayAmount);
        float swayY = Mathf.Clamp(-movementY * swayAmount, -maxSwayAmount, maxSwayAmount);

        // Apply the sway to the initial position
        Vector3 sway = new Vector3(swayX, swayY, 0f);
        Vector3 finalPosition = initialPosition + sway;

        // Apply the final position to the RectTransform
        hudRectTransform.anchoredPosition = Vector3.Lerp(hudRectTransform.anchoredPosition, finalPosition, Time.deltaTime * 5f);
    }
}
