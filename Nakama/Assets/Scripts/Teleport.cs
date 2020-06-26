using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{

  float delay = 1f;
  public string levelname = "SampleScene";
  Player player1;
  Player player2;
  Teleporter teleporter1;
  Teleporter teleporter2;
  CapsuleCollider2D capsuleCollider;

    void Start()
    {
      teleporter1 = transform.GetChild(0).gameObject.GetComponent<Teleporter>();
      teleporter2 = transform.GetChild(1).gameObject.GetComponent<Teleporter>();
    }

    // Update is called once per frame
    void Update()
    {
      nextLevel();
    }

    void nextLevel() {
      if (teleporter1.isReady && teleporter2.isReady) {
        teleporter1.teleporting(true);
        teleporter2.teleporting(true);
        delay -= Time.deltaTime;
      }
      else {
        teleporter1.teleporting(false);
        teleporter2.teleporting(false);
        delay = 1f;
      }
      if(delay<=0) {
        SceneManager.LoadScene(levelname);
      }
    }
}
