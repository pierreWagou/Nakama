using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public string playerTag;
    float delay = 2f;
    public float speed = 0.1f;
    Transform playerTransform;
    LayerMask wallLayerMask = 1 << 8;
    LayerMask playerLayerMask = 1 << 12;
    int direction = 0;
    bool facingRight = false;
    BoxCollider2D boxCollider;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start() {
      boxCollider = GetComponent<BoxCollider2D>();
      rb = GetComponent<Rigidbody2D>();
      playerTransform = GameObject.FindWithTag(playerTag).GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() {
      GameObject player = GameObject.FindWithTag(playerTag);
      if(player!=null) {
        playerTransform = player.GetComponent<Transform>();
        move();
        flip();
      }
    }

    void move() {
      if(!isUnreachable()) {
        rb.velocity = new Vector2(direction * 5, rb.velocity.y);
      }
      else {
        if(whereIsWall()) {
          direction = direction * -1;
        }
        rb.velocity = new Vector2(direction * 2, rb.velocity.y);
      }
    }

    bool isUnreachable() {
      if(!whereIsPlayer()) {
        delay -= Time.deltaTime;
      }
      else {
        delay = 2f;
      }
      return delay<0;
    }

    bool whereIsPlayer() {
      RaycastHit2D raycastHitRight = Physics2D.Raycast(boxCollider.bounds.center, Vector2.right, boxCollider.bounds.extents.y + 20, playerLayerMask);
      RaycastHit2D raycastHitLeft = Physics2D.Raycast(boxCollider.bounds.center, Vector2.left, boxCollider.bounds.extents.y + 20, playerLayerMask);
      return raycastHitRight.collider != null || raycastHitLeft.collider != null;
    }

    bool whereIsWall(float distance=1f) {
      if (facingRight) {
        RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider.bounds.center, Vector2.right, boxCollider.bounds.extents.y + distance, wallLayerMask);
        return raycastHit.collider != null;
      }
      else {
        RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider.bounds.center, Vector2.left, boxCollider.bounds.extents.y + distance, wallLayerMask);
        return raycastHit.collider != null;

      }
    }

    void flip() {
      if (!isUnreachable()) {
        direction = playerTransform.position.x+direction*5 < transform.position.x ? -1 : 1;
      }
      if (direction == -1 && facingRight || direction == 1 && !facingRight) {
        facingRight = !facingRight;
        transform.Rotate(0f,180f,0f);
      }
    }
}
