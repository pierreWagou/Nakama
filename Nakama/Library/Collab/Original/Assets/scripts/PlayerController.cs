using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public float speed = 5.0f;
  public float jumpHeight = 7.0f;
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
      jumping();
    }

    void FixedUpdate() {
      r2d.velocity = new Vector2(moveDirection * speed, r2d.velocity.y);
    }

    bool isPlayerGrounded() {
      RaycastHit2D raycastHit = Physics2D.Raycast(capsuleCollider.bounds.center, Vector2.down, capsuleCollider.bounds.extents.y + 0.01f, layerMask);
      return raycastHit.collider != null;
    }

    void jumping() {
      if (Input.GetKey(jump) && isPlayerGrounded()) {
        r2d.velocity = Vector2.up * jumpHeight;
      }
      else if (r2d.velocity.y > 0 && !Input.GetKey(jump)) {
        r2d.velocity += Vector2.up * Physics2D.gravity.y * 1f * Time.deltaTime;
      }
      else if (r2d.velocity.y < 0) {
        r2d.velocity += Vector2.up * Physics2D.gravity.y * 1.5f * Time.deltaTime;
      }
    }
}
