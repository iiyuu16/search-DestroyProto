using System;
using System.Collections;
using UnityEngine;

public class InitScan : MonoBehaviour
{
    public static InitScan instance;
    public event Action<float> OnRevealDurationOver; // Event for when the reveal duration is over
    public ParticleSystem scanner;
    public float scanDuration = 3f; // Duration of the scanning effect
    public float revealDuration = 3f; // Duration for which the target stays revealed
    public float maxSize = 30;
    public float growthRate = 1f;
    public float startingSize = 0.1f;
    public Material newMaterial; // New material to be applied

    private SphereCollider sphereCollider;
    private bool isExpanding = false;
    private Material originalMaterial; // Original material of the target

    private Coroutine revertMaterialCoroutine; // Coroutine reference for reverting material

    void Start()
    {
        instance = this;
        sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        sphereCollider.radius = startingSize;

        // Store the original material of the target
        Renderer targetRenderer = GetComponent<Renderer>();
        if (targetRenderer != null)
        {
            originalMaterial = targetRenderer.material;
        }
        else
        {
            Debug.LogWarning("Target does not have a Renderer component.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            scanner.Play();
            StartExpanding();
        }

        if (isExpanding)
        {
            ExpandCollider();
        }
    }

    void StartExpanding()
    {
        isExpanding = true;
    }

    void ExpandCollider()
    {
        if (sphereCollider.radius < maxSize)
        {
            sphereCollider.radius += growthRate * Time.deltaTime;
        }
        else
        {
            sphereCollider.radius = startingSize;
            isExpanding = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            Debug.Log("Target detected in range");

            // Apply the new material to the target
            Renderer targetRenderer = other.GetComponent<Renderer>();
            if (targetRenderer != null && newMaterial != null)
            {
                targetRenderer.material = newMaterial;

                // Cancel the previous coroutine if running
                if (revertMaterialCoroutine != null)
                {
                    StopCoroutine(revertMaterialCoroutine);
                }

                // Start a new coroutine to revert material after the reveal duration
                revertMaterialCoroutine = StartCoroutine(RevertMaterialAfterDelay(targetRenderer));
            }
            else
            {
                Debug.LogWarning("Target or new material is not assigned.");
            }
        }
    }

    IEnumerator RevertMaterialAfterDelay(Renderer targetRenderer)
    {
        yield return new WaitForSeconds(revealDuration);

        // Revert the material to the original after the reveal duration
        if (targetRenderer != null)
        {
            targetRenderer.material = originalMaterial;
            OnRevealDurationOver?.Invoke(revealDuration); // Invoke the event
        }
    }
}
