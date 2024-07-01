using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeValue = 60; // Initial time value in seconds
    public TextMeshProUGUI timeText; // Reference to the TextMeshProUGUI component
    public GameObject gameOverScreen; // Reference to the game over screen

    private void Start()
    {
        // Deactivate the game over screen at the start
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
    }

    private void Update()
    {
        // Update the time value
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
            DisplayTime(timeValue);
        }
        else
        {
            // If time runs out, trigger game over
            timeValue = 0;
            gameOverTrigger();
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        // Convert time to minutes, seconds, and milliseconds
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliseconds = (timeToDisplay - Mathf.FloorToInt(timeToDisplay)) * 1000;

        // Update the time text
        timeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    private void gameOverTrigger()
    {
        // Pause the game and activate the game over screen
        Time.timeScale = 0;
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
    }
}
