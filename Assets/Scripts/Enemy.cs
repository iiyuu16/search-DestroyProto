using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float revealDuration = 3f; 
    private bool isRevealed = false; 

    public void RevealEnemy()
    {
        isRevealed = true;
        Debug.Log("Enemy revealed!");
        Invoke("ResetRevealState", revealDuration);
    }

    public bool IsRevealed()
    {
        return isRevealed;
    }

    private void ResetRevealState()
    { 
        isRevealed = false;
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
            Destroy(gameObject);
        }
    }
}