using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI obtainedScoreText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
        UpdateObtainedScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString("D8");
        }
    }

    private void UpdateObtainedScoreText()
    {
        if (obtainedScoreText != null)
        {
            obtainedScoreText.text = "Obtained " + score.ToString("D8") + " Fr.";
        }
    }
}
