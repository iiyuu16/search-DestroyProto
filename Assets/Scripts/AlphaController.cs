using UnityEngine;

public class AlphaController : MonoBehaviour
{
    public Renderer objectRenderer; // Reference to the object's renderer
    public float enterAlpha = 70f; // Alpha value when the player enters
    public float exitAlpha = 255f; // Alpha value when the player exits

    private Color originalColor; // Original color of the object

    void Start()
    {
        // Store the original color of the object
        originalColor = objectRenderer.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Set the alpha value to enterAlpha when the player enters
            Color newColor = objectRenderer.material.color;
            newColor.a = enterAlpha / 255f; // Convert from 0-255 range to 0-1 range
            objectRenderer.material.color = newColor;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Set the alpha value to exitAlpha when the player exits
            Color newColor = objectRenderer.material.color;
            newColor.a = exitAlpha / 255f; // Convert from 0-255 range to 0-1 range
            objectRenderer.material.color = newColor;
        }
    }

    // Reset the object's transparency when the script is disabled
    private void OnDisable()
    {
        objectRenderer.material.color = originalColor;
    }
}
