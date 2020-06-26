using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 40;
    public GameObject impactEffectPrefab;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody2D>();
      rb.velocity = transform.right * speed;

    }

    void OnTriggerEnter2D(Collider2D col) {
      Health health = col.GetComponent<Health>();
      if(health!=null) {
        health.takeDamage(damage);
      }
      Instantiate(impactEffectPrefab, transform.position,  Quaternion.identity);
      Destroy(gameObject);
    }
}
