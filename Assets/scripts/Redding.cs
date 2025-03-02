using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
public class Redding : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color startColor = Color.white;
    private Color targetColor = Color.red;
    private float duration = 3f;   // Transition duration in seconds
    private float t = 0f;

    public AudioSource failSound;
    public AudioSource ahhSound;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = startColor;  // Start with white
        ahhSound.Play();
        failSound.Play();
    }

    void Update()
    {
        if (t < 1f)
        {
            t += Time.deltaTime / duration;  // Increment t based on duration
            spriteRenderer.color = Color.Lerp(startColor, targetColor, t);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.score = 0;
            SceneManager.LoadScene("game");
        }
    }
}
