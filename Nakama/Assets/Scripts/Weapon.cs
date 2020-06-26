using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    AudioSource audioSource;
    int _nbBullet = 0;
    public int maxBullet = 10;
    Transform firepoint;
    public GameObject bulletPrefab;
    bool _isShooting = false;

    // Start is called before the first frame update
    void Start()
    {
      firepoint = transform.GetChild(0).transform;
      audioSource = transform.GetChild(0).GetComponent<AudioSource>();
    }

    public void shoot() {
      if(_nbBullet > 0) {
        _nbBullet -= 1;
        audioSource.Play();
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
      }
    }

    public void addAmmo(int ammo) {
      _nbBullet += ammo;
      if(_nbBullet > maxBullet) {
        _nbBullet = maxBullet;
      }
    }

    public bool isShooting {
      set { _isShooting = value; }
    }

    public int nbBullet {
      get { return _nbBullet; }
    }
}
