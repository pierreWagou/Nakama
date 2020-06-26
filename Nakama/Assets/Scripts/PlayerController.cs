using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameObject playerPrefab1, playerPrefab2, playerObject1, playerObject2;

    // Start is called before the first frame update
    void Start()
    {
      playerPrefab1 = transform.GetChild(0).gameObject;
      playerPrefab2 = transform.GetChild(1).gameObject;
      playerObject1 = spawning(playerPrefab1);
      playerObject2 = spawning(playerPrefab2);
    }

    // Update is called once per frame
    void Update()
    {
      respawning(ref playerObject1, playerPrefab1);
      respawning(ref playerObject2, playerPrefab2);
    }

    public void respawning(ref GameObject playerObject, GameObject playerPrefab) {
      if (playerObject == null) {
        playerObject = spawning(playerPrefab);
      }
    }

    GameObject spawning(GameObject playerPrefab) {
      GameObject instance = Instantiate(playerPrefab, playerPrefab.transform.position, playerPrefab.transform.rotation) as GameObject;
      playerPrefab.SetActive(false);
      instance.SetActive(true);
      return instance;
    }
}
