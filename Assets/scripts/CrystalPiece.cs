using UnityEngine;

public class CrystalPiece : MonoBehaviour
{
  public float lifeTime;
  public Vector3 velocity = new Vector3(0, 0, 0);

  void Start()
  {
    Destroy(gameObject, lifeTime);
  }

  void Update()
  {
    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    if (rb != null)
    {
      rb.linearVelocity *= 0.999999f;  // Reduce speed slightly every frame for a slowing effect
    }
  }
}
