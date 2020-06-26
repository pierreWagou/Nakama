﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterAmmo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col) {
      if (col.tag == "Player2") {
        Weapon weapon = col.GetComponent<Weapon>();
        weapon.addAmmo(1);
        Destroy(gameObject);
      }
    }
}
