using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;  // Example variable
    public float position = 0;
    public float playX = 0;
    public float playY = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }
    public void setPosition(float pos)
    {
        position = pos;
    }
    public void setPlayX(float x)
    {
        playX = x;
    }
    public void setPlayY(float y)
    {
        playY = y;
    }
}
