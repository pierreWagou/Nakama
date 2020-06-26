using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activation : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      activate();
    }

    void activate() {
      gameObject.GetComponent<Renderer>().enabled = button.isOn;
      gameObject.GetComponent<BoxCollider2D>().enabled = button.isOn;
    }
}
