using UnityEngine;
using TMPro;

public class ShowScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the TMP Text component

    void Start()
    {
        UpdateScoreText(); // Initialize score text
    }

    public void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + GameManager.Instance.score;
        }
    }
}
