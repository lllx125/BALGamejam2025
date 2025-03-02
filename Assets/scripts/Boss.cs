using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Boss : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float timer = 0f;
    private float attackiInterval = 5f;

    public int health = 10;

    private Head head;
    void Start()
    {
        head = transform.Find("Head").GetComponent<Head>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= attackiInterval)
        {

            if (Random.Range(1, 3) == 1)
            {
                head.AttackUp();
            }
            else
            {
                head.AttackDown();
            }
            timer = 0;
            attackiInterval = Random.Range(4, 7);
        }
    }
    public void Hurt()
    {
        health--;
        if (health == 5)
        {
            transform.Find("bandage_1").GameObject().SetActive(true); // Show bandage 1
        }
        else if (health == 3)
        {
            transform.Find("bandage_2").GameObject().SetActive(true); // Show bandage 2
        }
        else if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        GameObject.Find("Player").GetComponent<BossPlayer>().Win();
    }
}
