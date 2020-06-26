using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public float timeVisible    = 0.3f;
    public float timeInvisible  = 0.3f;
    public float blinkFor       = 5.0f;
    public bool startState      = true;

    void Start()
    {
        StartCoroutine(blink());
    }

    IEnumerator blink()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.enabled = startState;

        while (true)
        {
            if (startState)
            {
                renderer.enabled = false;
                yield return new WaitForSeconds(timeInvisible);
                renderer.enabled = true;
                yield return new WaitForSeconds(timeVisible);
            }
            else
            {
                renderer.enabled = true;
                yield return new WaitForSeconds(timeVisible);
                renderer.enabled = false;
                yield return new WaitForSeconds(timeInvisible);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
      //If the player press Enter we load the first level
      if(Input.GetKeyDown (KeyCode.Return))
       {
          SceneManager.LoadScene("Level 1");
       }

    }
}
