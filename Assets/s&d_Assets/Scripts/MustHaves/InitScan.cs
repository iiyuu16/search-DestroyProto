using System;
using System.Collections;
using UnityEngine;

public class InitScan : MonoBehaviour
{
    public static InitScan instance;
    public event Action<float> OnRevealDurationOver;
    public ParticleSystem scanner;
    public float scanDuration = 3f;
    public float revealDuration = 3f;
    public float maxSize = 30;
    public float growthRate = 1f;
    public float startingSize = 0.1f;
    public Material newMaterial;

    private SphereCollider sphereCollider;
    private bool isExpanding = false;
    private Material originalMaterial;
    private PlayerMovement playerMovement; // Reference to PlayerMovement script

    private Coroutine revertMaterialCoroutine;

    public KeyCode Scan;
    public soundSource sfx;

    void Start()
    {
        instance = this;
        sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        sphereCollider.radius = startingSize;

        Renderer targetRenderer = GetComponent<Renderer>();
        if (targetRenderer != null)
        {
            originalMaterial = targetRenderer.material;
        }
        else
        {
            Debug.LogWarning("Target does not have a Renderer component.");
        }

        // Get reference to PlayerMovement script
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        // Check if player is not stunned and scan input is pressed
        if (!playerMovement.isStunned && Input.GetKeyDown(Scan))
        {
            scanner.Play();
            sfx.scanSFX();
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

            Renderer targetRenderer = other.GetComponent<Renderer>();
            if (targetRenderer != null && newMaterial != null)
            {
                Material originalMaterial = targetRenderer.material;
                targetRenderer.material = newMaterial;

                if (revertMaterialCoroutine != null)
                {
                    StopCoroutine(revertMaterialCoroutine);
                }

                revertMaterialCoroutine = StartCoroutine(RevertMaterialAfterDelay(targetRenderer, originalMaterial));
            }
            else
            {
                Debug.LogWarning("Target or new material is not assigned.");
            }
        }
    }

    IEnumerator RevertMaterialAfterDelay(Renderer targetRenderer, Material originalMaterial)
    {
        yield return new WaitForSeconds(revealDuration);

        if (targetRenderer != null)
        {
            targetRenderer.material = originalMaterial;
            OnRevealDurationOver?.Invoke(revealDuration);
        }
    }
}
