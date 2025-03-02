using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float timer = 0f;
    public float interval = 3f;

    private Head head;
    void Start()
    {
        head = transform.Find("Head").GetComponent<Head>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            Attack();

        }
    }

    void Attack()
    {
        head.Open();
        Invoke("EndAttack", 1f);
    }

    void EndAttack()
    {

        head.Close();
        timer = 0f; // Reset timer
    }
}
