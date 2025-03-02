using UnityEngine;

public class Head : MonoBehaviour
{


    public Sprite open; // Assign this in the Inspector
    public Sprite close;

    public GameObject player;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        LookAtPlayer();
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
}
