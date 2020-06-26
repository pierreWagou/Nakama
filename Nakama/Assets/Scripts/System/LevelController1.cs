using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController1 : MonoBehaviour
{
    GameController gameController;
    Terminal terminal;

    // Start is called before the first frame update
    void Start()
    {
      gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
      terminal = GameObject.Find("Content").GetComponent<Terminal>();

      welcomeMessage();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void welcomeMessage() {
      gameController.isPaused = true;
      terminal.executeSystem("message welcome");
    }
}
