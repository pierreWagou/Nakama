using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  PlayerController playerController;
  Health health;
  Animator animator;
  float speed = 5.0f;
  float jumpHeight = 7.0f;
  private bool facingRight = true;
  public KeyCode rightKey = KeyCode.D;
  public KeyCode leftKey = KeyCode.Q;
  public KeyCode jumpKey = KeyCode.Z;
  public KeyCode crouchKey = KeyCode.S;
  public KeyCode runKey = KeyCode.LeftShift;
  public KeyCode actKey = KeyCode.E;
  public KeyCode fireKey = KeyCode.A;
  Rigidbody2D r2d;
  CapsuleCollider2D capsuleCollider;
  Spawner spawner;
  Weapon weapon;
  Transform firepoint;
  LayerMask groundLayerMask = 1 << 8;
  LayerMask trapLayerMask = 1 << 9;
  LayerMask ladderLayerMask = 1 << 11;
  GameController gameController;
  int horizontalDirection = 0;
  int verticalDirection = 0;
  bool isJumping = false;
  bool isGrounded = true;
  bool isClimbing = false;
  bool isCrouching = false;
  bool isFalling = false;
  bool isShooting = false;
  bool _isActing = false;

    // Start is called before the first frame update
    void Start()
    {
      firepoint = transform.Find("Fire Point");
      r2d = GetComponent<Rigidbody2D>();
      capsuleCollider = GetComponent<CapsuleCollider2D>();
      spawner = GetComponent<Spawner>();
      weapon = GetComponent<Weapon>();
      health = GetComponent<Health>();
      animator = GetComponent<Animator>();
      gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
      playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
      if (!gameController.isPaused) {
        crouching();
        shooting();
        acting();
        dead();
      }
    }

    void FixedUpdate() {
      if (!gameController.isPaused) {
        grounding();
        climbing();
        jumping();
        moving();
        animate();
      }
    }

    void moving() {
      if (Input.GetKey(rightKey) || Input.GetKey(leftKey)) {
        horizontalDirection = Input.GetKey(rightKey) ? 1 : -1;
        speed = isGrounded ? 5.0f : 4.0f;
        if (Input.GetKey(runKey) && !isCrouching) {
          speed = isGrounded ? 7.0f : 5.0f;
        }
        flip();
      }
      else {
        horizontalDirection = 0;
      }
      r2d.velocity = new Vector2(horizontalDirection * speed, r2d.velocity.y);
    }

    void flip() {
      if (horizontalDirection == -1 && facingRight || horizontalDirection == 1 && !facingRight) {
        facingRight = !facingRight;
        transform.Rotate(0f,180f,0f);
      }
    }

    void jumping() {
      if (Input.GetKey(jumpKey) && isGrounded && !isClimbing) {
        isJumping = true;
        r2d.velocity = Vector2.up * jumpHeight;
        animator.SetTrigger("jump");
      }
      else if (r2d.velocity.y > 0 && !Input.GetKey(jumpKey)) {
        r2d.velocity += Vector2.up * Physics2D.gravity.y * 1.5f * Time.deltaTime;
      }
      else if (r2d.velocity.y < 0) {
        isJumping = false;
        r2d.velocity += Vector2.up * Physics2D.gravity.y * 1.5f * Time.deltaTime;
      }
    }

    void grounding() {
        isGrounded = whereIsGround();
        isFalling = !whereIsGround(1f);
    }

    bool whereIsGround(float distance=0.1f) {
      RaycastHit2D raycastHit = Physics2D.Raycast(capsuleCollider.bounds.center, Vector2.down, capsuleCollider.bounds.extents.y + distance, groundLayerMask);
      return raycastHit.collider != null;
    }

    void climbing() {
      RaycastHit2D raycastHit = Physics2D.Raycast(capsuleCollider.bounds.center, Vector2.up, capsuleCollider.bounds.extents.y + 0.01f, ladderLayerMask);
      if (raycastHit.collider!=null) {
        r2d.gravityScale = 0;
        if (Input.GetKey(jumpKey) || Input.GetKey(crouchKey)) {
          isClimbing = true;
          verticalDirection = Input.GetKey(jumpKey) ? 1 : -1;
        }
        else {
          isClimbing = !isGrounded;
          verticalDirection = 0;
        }
        r2d.velocity = new Vector2(r2d.velocity.x, verticalDirection * speed);
      }
      else {
        isClimbing = false;
        r2d.gravityScale = 1;
      }
    }

    void crouching() {
      float crouchingOffset = 0.5f;
      if(Input.GetKeyDown(crouchKey)) {
        isCrouching = true;
        capsuleCollider.size  = new Vector2(capsuleCollider.size.x, 1.25f);
        transform.position = new Vector3(transform.position.x, transform.position.y-crouchingOffset, transform.position.z);
        firepoint.position = new Vector3(firepoint.position.x, firepoint.position.y-crouchingOffset, firepoint.position.z);
      }
      else if (Input.GetKeyUp(crouchKey)) {
        isCrouching = false;
        capsuleCollider.size  = new Vector2(capsuleCollider.size.x, 2);
        transform.position = new Vector3(transform.position.x, transform.position.y+crouchingOffset, transform.position.z);
        firepoint.position = new Vector3(firepoint.position.x, firepoint.position.y+crouchingOffset, firepoint.position.z);
      }
    }

    void shooting() {
      isShooting = Input.GetKey(fireKey) && ! isJumping;
      if(Input.GetKeyDown(fireKey) && !isJumping) {
        weapon.shoot();
      }
    }

    void acting() {
      _isActing = Input.GetKeyUp(actKey);
    }

    void dead() {
      RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0f, Vector2.down, 0f, trapLayerMask);
      if (raycastHit.collider != null) {
        health.takeDamage(100);
      }
    }

    void animate() {
      animator.SetBool("isJumping", isJumping);
      animator.SetBool("isGrounded", isGrounded);
      animator.SetBool("isCrouching", isCrouching);
      animator.SetBool("isClimbing", isClimbing);
      animator.SetBool("isFalling", isFalling);
      animator.SetBool("isShooting", isShooting);
      float velocity = isClimbing ? r2d.velocity.y : r2d.velocity.x;
      animator.SetFloat("velocity", Mathf.Abs(velocity));
    }

    public bool isActing {
      get {return _isActing;}
    }
}
