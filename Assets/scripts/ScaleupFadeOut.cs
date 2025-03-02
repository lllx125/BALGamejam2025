using System.Collections;
using UnityEngine;

public class ScaleupFadeOut : MonoBehaviour
{
    public float duration = 0.5f; // Duration of the effect
    public float targetScale = 3f; // Final scale multiplier
    private Vector3 initialScale;
    private Renderer objectRenderer;
    private CanvasGroup uiCanvasGroup;
    private bool isUI = false;

    void Start()
    {
        initialScale = transform.localScale;

        // Check if the object is a UI element
        uiCanvasGroup = GetComponent<CanvasGroup>();
        if (uiCanvasGroup != null)
        {
            isUI = true;
        }
        else
        {
            objectRenderer = GetComponent<Renderer>();
        }

        StartCoroutine(ScaleAndFade());
    }

    IEnumerator ScaleAndFade()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            // Scale up
            transform.localScale = Vector3.Lerp(initialScale, initialScale * targetScale, t);

            // Fade out
            if (isUI)
            {
                uiCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t);
            }
            else if (objectRenderer != null)
            {
                foreach (Material mat in objectRenderer.materials)
                {
                    Color color = mat.color;
                    color.a = Mathf.Lerp(1f, 0f, t);
                    mat.color = color;
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final values are set
        transform.localScale = initialScale * targetScale;
        if (isUI)
        {
            uiCanvasGroup.alpha = 0f;
        }
        else if (objectRenderer != null)
        {
            foreach (Material mat in objectRenderer.materials)
            {
                Color color = mat.color;
                color.a = 0f;
                mat.color = color;
            }
        }

        Destroy(gameObject); // Optional: Destroy the object after effect
    }
}
