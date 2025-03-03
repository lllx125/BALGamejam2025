using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int score = 1;
    public TextMeshProUGUI scoreText;
    void Start()
    {
        scoreText.text = "+" + score;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
