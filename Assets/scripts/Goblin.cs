using UnityEngine;

public class Goblin : MonoBehaviour
{
    public int position = -1;
    public float jumpSpeed;
    private float timer = 0;

    public float lowerL; // y coordinate of lower edge of upper level
    public float upperL; // y coordinate of upper edge of lower level
    private Vector3 velocity = new Vector3(0, 0, 0);

    public AudioSource goblinSound;
    public AudioSource deathSound;
    void Start()
    {
        upperL = -lowerL;
        goblinSound.Play();

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.5f)
        {
            Jump();
            timer = 0;
        }
        transform.position += velocity * Time.deltaTime;
        Land();

    }
    void Jump()
    {
        if (position == -1)
        {
            velocity = new Vector3(velocity.x, jumpSpeed, 0);
            transform.localScale = new Vector3(0.2f, -0.2f, 1f);
        }
        else
        {
            velocity = new Vector3(velocity.x, -jumpSpeed, 0);
            transform.localScale = new Vector3(0.2f, 0.2f, 1f);

        }
        position = 0 - position;
    }
    void Land()
    {
        if (transform.position.y < lowerL)
        {
            transform.position = new Vector3(transform.position.x, lowerL, 0);
            velocity = new Vector3(velocity.x, 0, 0);
        }
        else if (transform.position.y > upperL)
        {
            transform.position = new Vector3(transform.position.x, upperL, 0);
            velocity = new Vector3(velocity.x, 0, 0);
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            deathSound.Play();
            Destroy(gameObject);
        }
    }
}

