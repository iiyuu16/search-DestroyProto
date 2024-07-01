using UnityEngine;
using TMPro;

public class sdScoreManager : MonoBehaviour
{
    public static sdScoreManager instance;
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
            scoreText.text = FormatScore(score);
        }
    }

    private void UpdateObtainedScoreText()
    {
        if (obtainedScoreText != null)
        {
            obtainedScoreText.text = "Obtained " + FormatScore(score) + " Fr.";
        }
    }

    private string FormatScore(int score)
    {
        string scoreStr = score.ToString();
        return scoreStr.PadLeft(8, ' ');
    }
}
