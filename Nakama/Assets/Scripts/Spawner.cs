using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
  public GameObject template;
  public float x = -20;
  public float y = 2;


    public void spawning() {
      GameObject newInstance = Instantiate(template, new Vector2(x,y), transform.rotation);
      newInstance.SetActive(true);
    }
}
