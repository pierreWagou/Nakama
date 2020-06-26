using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    GameObject cityBoss, caveBoss;

    // Start is called before the first frame update
    void Start()
    {
      cityBoss = GameObject.FindGameObjectsWithTag("Boss")[0];
      caveBoss = GameObject.FindGameObjectsWithTag("Boss")[1];
    }

    // Update is called once per frame
    void Update()
    {
      if(cityBoss==null && caveBoss==null) {
        SceneManager.LoadScene("Game Over");
      }
    }
}
