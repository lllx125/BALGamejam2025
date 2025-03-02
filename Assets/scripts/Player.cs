using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour

{
    private int jumpCount = 0;
    public float jumpSpeed;
    public float dashSpeed;
    public float runSpeed;

    private bool isDash = false;


    public float lowerL; // y coordinate of lower edge of upper level
    public float upperL; // y coordinate of upper edge of lower level
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool position = true;
    private float xPosition = -7f;

    private Vector3 velocity = new Vector3(0, 0, 0);
    public AudioSource dashSound;
    public AudioSource jumpSound;
    public AudioSource runSound;

    private GenerateGamePlay generateGamePlay;

    void Start()
    {
        upperL = -lowerL;
        transform.position = new Vector3(xPosition, lowerL, 0);
        generateGamePlay = GameObject.Find("GenerateGamePlay").GetComponent<GenerateGamePlay>();
        SetSpeed(runSpeed);
        runSound.Play();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpCount = (jumpCount + 1) % 3;
            if (jumpCount == 0)
            {
                Dash();
            }
            else
            {
                jumpSound.Play();
                Jump();
            }
        }
        transform.position += velocity * Time.deltaTime;
        Land();
        DashRange();

    }
    void SetSpeed(float speed)
    {
        generateGamePlay.speed = speed;
    }
    void Dash()
    {
        isDash = true;
        runSound.Stop();
        dashSound.Play();
        SetSpeed(dashSpeed * 0.3f);
        velocity = new Vector3(dashSpeed * 0.7f, 0, 0);
        Invoke("BackDash", 0.5f);
    }

    void BackDash()
    {
        velocity = new Vector3(-0.8f * runSpeed, 0, 0);
        SetSpeed(0.8f * runSpeed + runSpeed);
        Fall();
        isDash = false;
        runSound.Play();
    }

    void DashRange()
    {
        if (transform.position.x > 9)
        {
            BackDash();
        }
        else if (transform.position.x < xPosition - 0.01)
        {
            velocity = new Vector3(0, velocity.y, 0);
            transform.position = new Vector3(xPosition, transform.position.y, 0);
            SetSpeed(runSpeed);

        }
    }

    void Jump()
    {
        SetSpeed(runSpeed);
        if (position)
        {
            velocity = new Vector3(velocity.x, jumpSpeed, 0);
            transform.localScale = new Vector3(0.2f, -0.2f, 1f);
        }
        else
        {
            velocity = new Vector3(velocity.x, -jumpSpeed, 0);
            transform.localScale = new Vector3(0.2f, 0.2f, 1f);

        }
        position = !position;
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
    void Fall()
    {
        position = transform.position.y > 0;
        Jump();
    }

    public void Die()
    {
        SceneManager.LoadScene("die");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(isDash);
        Debug.Log(other.tag);
        switch (other.tag)
        {
            case "obstacle":
                Die();
                break;
            case "crystal":
                GameManager.Instance.AddScore(2);
                break;
            case "goblin":
                if (isDash)
                {
                    GameManager.Instance.AddScore(1);
                }
                else
                {
                    Die();
                }
                break;
        }
    }
}
