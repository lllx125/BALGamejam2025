using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class BossPlayer : MonoBehaviour

{
    private int jumpCount = 0;
    public float jumpSpeed;
    public float dashSpeed;

    public float lowerL; // y coordinate of lower edge of upper level
    public float upperL; // y coordinate of upper edge of lower level
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool position = true;
    private float xPosition = -7f;

    private float attackXPosition = 3f;

    private bool isWin = false;

    private Vector3 velocity = new Vector3(0, 0, 0);

    public GameObject boss;

    public AudioSource dashSound;
    public AudioSource jumpSound;

    void Start()
    {
        upperL = -lowerL;
        transform.position = new Vector3(xPosition, lowerL, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isWin)
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

    void Dash()
    {
        dashSound.Play();
        velocity = new Vector3(dashSpeed, 0, 0);
        if (position)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
    }

    void BackDash()
    {

        velocity = new Vector3(-0.5f * dashSpeed, 0, 0);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    void Jump()
    {

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
            if (isWin)
            {
                SceneManager.LoadScene("game");
            }
        }
        else if (transform.position.y > upperL)
        {
            transform.position = new Vector3(transform.position.x, upperL, 0);
            velocity = new Vector3(velocity.x, 0, 0);
        }

    }

    void DashRange()
    {
        if (transform.position.x > attackXPosition)
        {
            transform.position = new Vector3(attackXPosition, transform.position.y, 0);
            if (boss != null)
            {
                boss.GetComponent<Boss>().Hurt();
            }
            BackDash();
        }
        else if (transform.position.x < xPosition - 0.5)
        {
            velocity = new Vector3(0, velocity.y, 0);
            transform.position = new Vector3(xPosition, transform.position.y, 0);
            Fall();
        }
    }
    void Fall()
    {
        if (isWin)
        {
            velocity = new Vector3(velocity.x, -jumpSpeed, 0);
            transform.localScale = new Vector3(0.2f, 0.2f, 1f);
            return;
        }

        position = transform.position.y > 0;
        Jump();
    }

    public void Die()
    {
        SceneManager.LoadScene("die");
    }

    public void Win()
    {
        GameManager.Instance.AddScore(10);
        isWin = true;
    }

}
