using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    AudioSource audioSource;
    public int health = 100;
    public GameObject deathEffect;

    void Start() {
      audioSource = GetComponent<AudioSource>();
    }

    public void takeDamage(int damage) {
      health -= damage;
      if(health<=0) {
        die();
      }
    }

    void die() {
      audioSource.Play();
      Instantiate(deathEffect, transform.position, Quaternion.identity);
      transform.position = new Vector2(400, 400);
      Destroy(gameObject, 1f);
    }
}
