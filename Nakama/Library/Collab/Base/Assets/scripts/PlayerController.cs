using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public float speed = 5.0f;
  public float jumpHeight = 5.0f;
  public bool isGrounded = false;
  public KeyCode right = KeyCode.D;
  public KeyCode left = KeyCode.Q;
  public KeyCode jump = KeyCode.Z;
  Rigidbody2D r2d;
  CapsuleCollider2D capsuleCollider;
  LayerMask layerMask = 1 << 8;
  int moveDirection = 0;

    // Start is called before the first frame update
    void Start()
    {
      r2d = GetComponent<Rigidbody2D>();
      capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKey(right) || Input.GetKey(left)) {
        moveDirection = Input.GetKey(right) ? 1 : -1;
      }
      else {
        moveDirection = 0;
      }
      if (Input.GetKey(jump) && isPlayerGrounded()) {
        r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
      }
    }

    void FixedUpdate() {
      r2d.velocity = new Vector2(moveDirection * speed, r2d.velocity.y);
      isGrounded = isPlayerGrounded();
    }

    bool isPlayerGrounded() {
      RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0f, Vector2.down, 0.1f, layerMask);
      return raycastHit.collider != null;
    }
}
