using System.Collections;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 5f;      // Movement speed
    public float duration = 2f;   // Duration in seconds

    void Start()
    {
        StartCoroutine(MoveLeftForTime());
    }

    IEnumerator MoveLeftForTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
