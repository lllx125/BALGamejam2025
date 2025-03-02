using UnityEngine;

public class Redding : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color startColor = Color.white;
    private Color targetColor = Color.red;
    private float duration = 3f;   // Transition duration in seconds
    private float t = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = startColor;  // Start with white
    }

    void Update()
    {
        if (t < 1f)
        {
            t += Time.deltaTime / duration;  // Increment t based on duration
            spriteRenderer.color = Color.Lerp(startColor, targetColor, t);
        }
    }
}
