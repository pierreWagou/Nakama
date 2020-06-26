using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicles : MonoBehaviour
{

  public float yMin = 4f;
  float speed = 20f;
  int direction = 1;
  float y;
  float scale;

    // Start is called before the first frame update
    void Start()
    {
      direction = Random.Range(0, 2);
      if (direction == 0) {
        direction = -1;
      }
      float x = Random.Range(-50f, 50f);
      randomTraffic();
      transform.position = new Vector3(x, y, transform.position.z);
      transform.localScale = new Vector2(direction*scale, scale);

    }

    // Update is called once per frame
    void Update()
    {
      Vector3 movement = new Vector2(direction,0);
      transform.Translate(movement*speed*Time.deltaTime);
      if (Mathf.Abs(transform.position.x) > 50) {
        direction = direction * -1;
        randomTraffic();
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        transform.localScale = new Vector2(direction*scale, scale);
      }
    }

    void randomTraffic() {
      speed = Random.Range(10,50);
      y = Random.Range(yMin,10f);
      scale = Random.Range(0.25f,1f);
    }
}
