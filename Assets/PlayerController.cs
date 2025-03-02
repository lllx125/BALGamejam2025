using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{
  public float jumpSwitchDuration = 0.3f;
  public float dashSpeed = 5f;
  public float dashDuration = 0.3f;
  private float lowerL = -4.6358f; // y coordinate of lower edge of upper level
  private float upperL = 4.6358f; // y coordinate of upper edge of lower level
  public float leftBound = -(17.68131f / 2); // x coordinate of left bound
  public float rightBound = 17.68131f / 2; // x coordinate of right bound

  public NewEmptyCSharpScript jumpUp;

  private int jumpCount = 0;
  private bool isDashing = false;
  private bool isVerticalMoving = false;
  private bool isInvincible = false;
  private float offsetY = 0.525f;
  private float halfHeight;
  private Rigidbody2D rb;
  private SpriteRenderer spriteRenderer;
  private Coroutine verticalMoveCoroutine = null;
  private bool interruptVertical = false;
  private float jumpSpeed = (9.9755703f - 0.6884272f * 2) / 0.3f;


  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();

    Bounds bounds = spriteRenderer.bounds;
    halfHeight = (bounds.max.y - bounds.min.y) / 2;

    Vector3 pos = transform.position;
    transform.position = new Vector3(leftBound + 1, lowerL + halfHeight + 0.525f, pos.z);
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      // decide if 超级无敌大横扫
      if ((jumpCount + 1) % 3 == 0)
      {
        if (verticalMoveCoroutine != null)
        {
          interruptVertical = true;
        }
        StartCoroutine(DashCoroutine());
      }
      else
      {
        if (!isVerticalMoving && !isDashing)
        {
          jumpCount++;
          verticalMoveCoroutine = StartCoroutine(VerticalMoveCoroutine());
        }
      }
    }
  }

  // IEnumerator HandleJumpSwitch()
  // {
  //   jumpCount++;
  //   isVerticalMoving = true;

  //   float currentY = transform.position.y;
  //   float lowerPosY = lowerL + halfHeight + offsetY;
  //   float upperPosY = upperL - halfHeight - offsetY;

  //   bool isOnLower = Mathf.Abs(currentY - lowerPosY) < Mathf.Abs(currentY - upperPosY);
  //   float targetY = isOnLower ? upperPosY : lowerPosY;

  //   float elapsed = 0f;
  //   Vector3 startPos = transform.position;
  //   Vector3 targetPos = new Vector3(startPos.x, targetY, startPos.z);

  //   while (elapsed < jumpSwitchDuration)
  //   {
  //     transform.position = Vector3.Lerp(startPos, targetPos, elapsed / jumpSwitchDuration);
  //     elapsed += Time.deltaTime;
  //     yield return null;
  //   }
  //   transform.position = targetPos;

  //   isVerticalMoving = false;
  // }

  IEnumerator VerticalMoveCoroutine()
  {
    interruptVertical = false;

    float currentY = transform.position.y;
    float lowerPos = lowerL + halfHeight + offsetY;
    float upperPos = upperL - halfHeight - offsetY;

    bool closerToLower = Mathf.Abs(currentY - lowerPos) < Mathf.Abs(currentY - upperPos);
    float targetY = closerToLower ? upperPos : lowerPos;

    float elapsed = 0f;
    float duration = jumpSwitchDuration;
    Vector3 startPos = transform.position;
    Vector3 endPos = new Vector3(startPos.x, targetY, startPos.z);

    while (elapsed < duration)
    {
      if (interruptVertical)
      {
        verticalMoveCoroutine = null;
        yield break;
      }

      transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
      elapsed += Time.deltaTime;
      yield return null;
    }
    transform.position = endPos;

    verticalMoveCoroutine = null;
  }

  // IEnumerator DashCoroutine()
  // {
  //   jumpCount++;
  //   isDashing = true;
  //   isInvincible = true;
  //   bool interruptDash = false;

  //   float originalGravity = rb.gravityScale;
  //   rb.gravityScale = 0;

  //   rb.linearVelocity = new Vector2(dashSpeed, 0);

  //   float elapsed = 0f;

  //   while (elapsed < dashDuration)
  //   {
  //     if (Input.GetKeyDown(KeyCode.Space))
  //     {
  //       interruptDash = true;
  //       break;
  //     }

  //     elapsed += Time.deltaTime;
  //     yield return null;
  //   }

  //   rb.linearVelocity = Vector2.zero;
  //   isDashing = false;
  //   isInvincible = false;

  //   if (interruptDash)
  //   {
  //     // NEW: If dash was interrupted, move to the opposite (farther) platform
  //     StartCoroutine(MoveToFartherPlatformCoroutine());
  //   }
  //   else
  //   {
  //     // Default behavior: move to nearest platform
  //     StartCoroutine(MoveToNearstPlatformCoroutine());
  //   }
  // }

  IEnumerator DashCoroutine()
  {
    jumpCount++;     // Count the jump
    isDashing = true;
    isInvincible = true;

    float originalGravity = rb.gravityScale;
    rb.gravityScale = 0;

    rb.linearVelocity = new Vector2(dashSpeed, 0);

    yield return new WaitForSeconds(dashDuration);

    rb.linearVelocity = Vector2.zero;
    rb.gravityScale = originalGravity;

    isDashing = false;
    isInvincible = false;

    StartCoroutine(MoveToNearstPlatformCoroutine());
  }

  IEnumerator MoveToNearstPlatformCoroutine()
  {
    float currentY = transform.position.y;
    float lowerPos = lowerL + halfHeight + offsetY;
    float upperPos = upperL - halfHeight - offsetY;

    bool closerToLower = Mathf.Abs(currentY - lowerPos) < Mathf.Abs(currentY - upperPos);
    float targetY = closerToLower ? lowerPos : upperPos;

    float elapsed = 0f;
    float duration = jumpSwitchDuration * (Math.Abs(rb.position.y - 0) / (upperL - lowerL - halfHeight * 2));
    Vector3 startPos = transform.position;
    Vector3 endPos = new Vector3(startPos.x, targetY, startPos.z);

    while (elapsed < duration)
    {
      transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
      elapsed += Time.deltaTime;
      yield return null;
    }
    transform.position = endPos;
  }


  IEnumerator MoveToFartherPlatformCoroutine()
  {
    float currentY = transform.position.y;
    float lowerPos = lowerL + halfHeight + offsetY;
    float upperPos = upperL - halfHeight - offsetY;

    bool closerToLower = Mathf.Abs(currentY - lowerPos) < Mathf.Abs(currentY - upperPos);
    float targetY = closerToLower ? upperPos : lowerPos;

    float elapsed = 0f;
    float duration = jumpSwitchDuration * (Math.Abs(rb.position.y - 0) / (upperL - lowerL - halfHeight * 2));
    Vector3 startPos = transform.position;
    Vector3 endPos = new Vector3(startPos.x, targetY, startPos.z);

    while (elapsed < duration)
    {
      transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
      elapsed += Time.deltaTime;
      yield return null;
    }
    transform.position = endPos;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Enemy") && isInvincible)
    {
      Destroy(collision.gameObject);
    }
  }
}