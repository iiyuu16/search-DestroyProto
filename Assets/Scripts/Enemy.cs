using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float revealDuration = 3f;
    private bool isRevealed = false;
    public GameObject shieldObject; // Reference to the shield object
    public int shieldHealth = 10; // Health of the shield
    public GameObject vulnerableObject; // Reference to the vulnerable object

    public void Start()
    {
        shieldObject.SetActive(false);
        //vulnerableObject.SetActive(false); // Start with the vulnerable object disabled
    }

    public void RevealEnemy()
    {
        isRevealed = true;
        Debug.Log("Enemy revealed!");
        shieldObject.SetActive(true); // Enable the shield object when revealed
        Invoke("ResetRevealState", revealDuration);
    }

    public bool IsRevealed()
    {
        return isRevealed;
    }

    private void ResetRevealState()
    {
        isRevealed = false;
        shieldObject.SetActive(false); // Disable the shield object when hidden
        Debug.Log("Enemy hidden!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Scanner"))
        {
            Debug.Log("Scanner detected!");
            RevealEnemy();
        }

        if (other.CompareTag("PlayerAttk") && isRevealed)
        {
            Debug.Log("Player attacked the revealed enemy!");
            // Reduce shield health when attacked by the player
            if (shieldHealth > 0)
            {
                shieldHealth--;
                Debug.Log("Shield health: " + shieldHealth);
                if (shieldHealth <= 0)
                {
                    // Destroy the shield object if shield health reaches 0
                    Destroy(shieldObject);
                    vulnerableObject.SetActive(true); // Enable the vulnerable object
                }
            }
            else
            {
                // If shield is already destroyed, destroy the vulnerable object directly
                Destroy(vulnerableObject);
            }
        }
    }
}