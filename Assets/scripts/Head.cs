using UnityEngine;

public class Head : MonoBehaviour
{


    public Sprite open; // Assign this in the Inspector
    public Sprite close;

    public GameObject player;

    private SpriteRenderer spriteRenderer;

    public GameObject fireup;
    public GameObject firedown;

    private int isAttacking = 0; // 0 = not attacking, -1 = attacking down warn, 1 = attacking up warn, -2 = attacking down, 2 = attacking ups

    public float attackTime = 1f;
    public float warnTime = 1f;

    public AudioSource fireSound;
    public AudioSource breathSound;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fireup.SetActive(false);
        firedown.SetActive(false);

        breathSound.Play();
    }
    void Update()
    {
        if (isAttacking == 0)
        {
            LookAtPlayer();
        }
        else
        {
            breathSound.Stop();
        }
        IsHit();
    }

    public void Open()
    {
        spriteRenderer.sprite = open;
    }

    public void Close()
    {
        spriteRenderer.sprite = close;
    }

    public void LookAtPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    public void AttackUp()
    {
        isAttacking = 1;
        Vector3 direction = new Vector3(0f, 3.25f, 0f) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Open();
        Invoke("Fire", warnTime);
    }


    public void AttackDown()
    {
        isAttacking = -1;
        Vector3 direction = new Vector3(0f, -3.25f, 0f) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Open();
        Invoke("Fire", warnTime);
    }
    void Fire()
    {
        fireSound.Play();
        isAttacking *= 2;
        if (isAttacking == 2)
        {
            fireup.SetActive(true);
        }
        else
        {
            firedown.SetActive(true);
        }
        Invoke("EndAttack", attackTime);
    }
    private void EndAttack()
    {
        isAttacking = 0;
        Close();
        fireup.SetActive(false);
        firedown.SetActive(false);
        breathSound.Play();
    }

    public void IsHit()
    {
        if (isAttacking == 2 && player.transform.position.y >= player.GetComponent<BossPlayer>().upperL - 0.05f)
        {
            player.GetComponent<BossPlayer>().Die();
        }
        if (isAttacking == -2 && player.transform.position.y <= player.GetComponent<BossPlayer>().lowerL + 0.05f)
        {
            player.GetComponent<BossPlayer>().Die();
        }
    }
}
