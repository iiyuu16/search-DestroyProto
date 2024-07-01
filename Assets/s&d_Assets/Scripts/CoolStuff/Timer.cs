using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeValue = 60;
    public TextMeshProUGUI timeText;
    public GameObject gameOverScreen;

    private void Start()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
    }

    private void Update()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
            DisplayTime(timeValue);
        }
        else
        {
            timeValue = 0;
            gameOverTrigger();
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliseconds = (timeToDisplay - Mathf.FloorToInt(timeToDisplay)) * 1000;

        timeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    private void gameOverTrigger()
    {
        Time.timeScale = 0;
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
    }
}
