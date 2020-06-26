using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMusicScript : MonoBehaviour
{
   static bool AudioBegin = false;

    void Awake() {
      if (!AudioBegin) {
        GetComponent<AudioSource>().Play();
        DontDestroyOnLoad(gameObject);
        AudioBegin = true;
      }
    }

    // Update is called once per frame
    void Update() {
    }
}
