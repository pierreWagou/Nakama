using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletHUD : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
      initBullet();
    }

    // Update is called once per frame
    void Update()
    {
      updateBullet();
    }

    void initBullet() {
      foreach(Transform child in transform) {
        child.gameObject.SetActive(false);
      }
    }

    void updateBullet() {
      GameObject player = GameObject.FindWithTag("Player1");
      if(player!=null) {
        Weapon weapon = player.GetComponent<Weapon>();
        initBullet();
        for(int i=0;i<weapon.nbBullet;i++) {
          transform.GetChild(i).gameObject.SetActive(true);
        }
      }
    }
}
