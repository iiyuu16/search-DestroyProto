using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float revealDuration = 3f;
    private bool isRevealed = false;
    public GameObject shieldObject;
    public int shieldHealth = 10;
    public GameObject vulnerableObject;
    public ParticleSystem hitFX;
    public ParticleSystem sparksFX;
    public ParticleSystem flashFX;
    public ParticleSystem fireFX;
    public ParticleSystem smokeFX;

    private Transform parentTransform; // Reference to the parent's transform

    public soundSource sfx;

    private void Start()
    {
        shieldObject.SetActive(false);
        // Get the parent's transform
        parentTransform = transform.parent;
    }

    public void RevealEnemy()
    {
        isRevealed = true;
        sfx.revealSFX();
        shieldObject.SetActive(true);
        Invoke("ResetRevealState", revealDuration);
    }

    public bool IsRevealed()
    {
        return isRevealed;
    }

    private void ResetRevealState()
    {
        isRevealed = false;
        shieldObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Scanner"))
        {
            RevealEnemy();
        }

        if (other.CompareTag("PlayerAttk") && isRevealed)
        {
            if (shieldHealth > 0)
            {
                shieldHealth--;
                hitFX.Play();
                sfx.hitSFX();
                if (shieldHealth <= 0)
                {
                    Destroy(shieldObject);
                    hitFX.Play();
                    sfx.hitSFX();
                    vulnerableObject.SetActive(true);
                }
            }
            else
            {
                sparksFX.Play();
                smokeFX.Play();
                flashFX.Play();
                fireFX.Play();
                sfx.explosionSFX();
                Destroy(vulnerableObject);
                // Check if the parent exists and destroy it if it does
                if (parentTransform != null)
                {
                    Destroy(parentTransform.gameObject);
                }
            }
        }
    }
}
