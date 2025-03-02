using UnityEngine;

public class Crystal : MonoBehaviour
{
  public GameObject[] pieces = new GameObject[5];
  public int position = -1;
  private float timer = 0;

  public float lowerL; // y coordinate of lower edge of upper level
  public float upperL; // y coordinate of upper edge of lower level
  private Vector3 velocity = new Vector3(0, 0, 0);
  private float lifeTime = 1f;
  public float speed = 3f;
  void Start()
  {
    upperL = -lowerL;
  }

  // Update is called once per frame
  void Update()
  {
    timer += Time.deltaTime;
    if (timer >= 0.5f)
    {
      timer = 0;
    }
    transform.position += velocity * Time.deltaTime;
  }

  // function where make the crystal breaks when the player touches it, the orignal Crystone will be dispeared and a new one will be generated. The new one is CrystalBreak, noting it is another prefab.
  void breaks()
  {
    for (int i = 0; i < 10; i++)
    {
      int index = Random.Range(0, 5);

      float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
      float distance = Random.Range(0.3f, 0.7f);

      float offsetX = Mathf.Cos(angle) * distance;
      float offsetY = Mathf.Sin(angle) * distance;

      GameObject crystalPiece = Instantiate(
          pieces[index],
          new Vector3(transform.position.x + offsetX, transform.position.y + offsetY, 0),
          Quaternion.Euler(0, 0, Random.Range(0, 360))
      );

      float randomScale = Random.Range(0.15f, 0.25f);
      crystalPiece.transform.localScale = new Vector3(randomScale, randomScale, 1f);

      Rigidbody2D rb = crystalPiece.AddComponent<Rigidbody2D>();
      rb.gravityScale = 0;

      Vector2 velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed + new Vector2(Random.Range(0f, 1f), 0);
      rb.AddForce(velocity, ForceMode2D.Impulse);


      CrystalPiece pieceScript = crystalPiece.AddComponent<CrystalPiece>();
      pieceScript.lifeTime = lifeTime;
    }
  }


  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      gameObject.tag = "untouchable";
      breaks();
      Destroy(gameObject);
    }
  }
}

