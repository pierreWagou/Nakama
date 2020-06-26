using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    GameObject ui;
    bool _isPaused = false;

      // Start is called before the first frame update
      void Start()
      {
        ui = GameObject.Find("Terminal");
      }

      // Update is called once per frame
      void Update()
      {
        displayTerminal();
      }

      void displayTerminal() {
        if(Input.GetKeyUp(KeyCode.Tab)) {
          _isPaused = !_isPaused;
        }
        Time.timeScale = _isPaused ? 0 : 1;
        ui.SetActive(_isPaused);
      }

      public bool isPaused {
        get {return _isPaused;}
        set {_isPaused = value;}
      }
}
