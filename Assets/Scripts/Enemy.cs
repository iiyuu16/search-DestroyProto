using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float revealDuration = 3f;
    private bool isRevealed = false;
    public GameObject shieldObject;
    public int shieldHealth = 10;
    public GameObject vulnerableObject;

    public void Start()
    {
        shieldObject.SetActive(false);
    }

    public void RevealEnemy()
    {
        isRevealed = true;
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
                if (shieldHealth <= 0)
                {
                    Destroy(shieldObject);
                    vulnerableObject.SetActive(true);
                }
            }
            else
            {
                Destroy(vulnerableObject);
            }
        }
    }
}
