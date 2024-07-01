using UnityEngine;

public class AlphaController : MonoBehaviour
{
    public Renderer objectRenderer;
    public float enterAlpha = 70f;
    public float exitAlpha = 255f;

    private Color originalColor;

    void Start()
    {
        originalColor = objectRenderer.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Color newColor = objectRenderer.material.color;
            newColor.a = enterAlpha / 255f;
            objectRenderer.material.color = newColor;
        }
    }

    private void OnTriggerExit(Collider other)
    {        
        if (other.CompareTag("Player"))
        {
            Color newColor = objectRenderer.material.color;
            newColor.a = exitAlpha / 255f;
            objectRenderer.material.color = newColor;
        }
    }

    private void OnDisable()
    {
        objectRenderer.material.color = originalColor;
    }
}
