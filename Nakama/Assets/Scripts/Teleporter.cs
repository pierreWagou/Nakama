using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
  LayerMask playerLayerMask = 1 << 12;
  CapsuleCollider2D capsuleCollider;
  GameObject effect;
  private bool _isReady = false;

    // Start is called before the first frame update
    void Start()
    {
      effect = transform.GetChild(1).gameObject;
      capsuleCollider = transform.GetChild(0).gameObject.GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
      ready();
    }

    void ready() {
      RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0f, Vector2.up, 0f, playerLayerMask);
      _isReady = raycastHit.collider != null;
    }

    public void teleporting(bool state) {
      effect.SetActive(state);
    }

    public bool isReady {
      get { return _isReady;}
    }
}
