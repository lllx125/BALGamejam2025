using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class PlayerTransit : MonoBehaviour

{
    public float jumpSpeed;
    public float dashSpeed;
    public float runSpeed;


    public float lowerL; // y coordinate of lower edge of upper level
    public float upperL; // y coordinate of upper edge of lower level
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool position = true;
    private float xPosition = -7f;

    private Vector3 velocity = new Vector3(0, 0, 0);
    public AudioSource dashSound;
    public AudioSource jumpSound;
    public AudioSource runSound;


    void Start()
    {
        upperL = -lowerL;
        transform.position = new Vector3(GameManager.Instance.playX, GameManager.Instance.playY, 0);
        BackDash();
        runSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Land();
        DashRange();
        transform.position += velocity * Time.deltaTime;
    }

    void Dash()
    {
        runSound.Stop();
        dashSound.Play();
        velocity = new Vector3(dashSpeed * 0.7f, 0, 0);
        Invoke("BackDash", 0.5f);
    }

    void BackDash()
    {
        velocity = new Vector3(-0.8f * runSpeed, 0, 0);
        Fall();
        runSound.Play();
    }

    void DashRange()
    {
        if (transform.position.x < xPosition - 0.01)
        {
            velocity = new Vector3(0, velocity.y, 0);
            transform.position = new Vector3(xPosition, transform.position.y, 0);
        }
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
        velocity = new Vector3(velocity.x, -jumpSpeed, 0);
        transform.localScale = new Vector3(0.2f, 0.2f, 1f);
    }
}
