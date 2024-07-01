using UnityEngine;

public class sdWinConditionManager : MonoBehaviour
{
    public GameObject[] targetGameObjects;
    public GameObject winScreen;

    private bool hasWon = false;

    private void Update()
    {
        if (!hasWon && AreAllTargetsDestroyed())
        {
            if (winScreen != null)
            {
                winScreen.SetActive(true);
                Time.timeScale = 0f;
            }

            hasWon = true;
        }
    }

    private bool AreAllTargetsDestroyed()
    {
        foreach (GameObject target in targetGameObjects)
        {
            if (target != null)
            {
                return false;
            }
        }
        return true;
    }
}
