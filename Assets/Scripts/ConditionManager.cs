using UnityEngine;

public class WinConditionManager : MonoBehaviour
{
    public GameObject[] targetGameObjects; // Array to hold all target GameObjects
    public GameObject winScreen; // Reference to the win screen object

    private bool hasWon = false;

    private void Update()
    {
        // Check if all target GameObjects are destroyed
        if (!hasWon && AreAllTargetsDestroyed())
        {
            // Display the win screen
            if (winScreen != null)
            {
                winScreen.SetActive(true);
                Time.timeScale = 0f;
            }

            // Set hasWon to true to prevent displaying win screen multiple times
            hasWon = true;
        }
    }

    private bool AreAllTargetsDestroyed()
    {
        // Iterate through all target GameObjects
        foreach (GameObject target in targetGameObjects)
        {
            // If any target is still active (not destroyed), return false
            if (target != null)
            {
                return false;
            }
        }
        // If all target GameObjects are destroyed, return true
        return true;
    }
}
