using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    // bool isSpawning = false;
    public GameObject gunAmmoPrefab;
    public GameObject blasterAmmoPrefab;
    public float delay;
    List<GameObject> ammoList;
    List<Vector3> spawnList;
    List<GameObject> prefabList;
    List<float> timerList;
    List<bool> isSpawningList;

    // Start is called before the first frame update
    void Start()
    {
      ammoList = new List<GameObject>();
      prefabList = new List<GameObject>();
      spawnList = new List<Vector3>();
      timerList = new List<float>();
      isSpawningList = new List<bool>();
      initList();
    }

    // Update is called once per frame
    void Update()
    {
      for(int i=0;i<ammoList.Count;i++) {
          timer(i);
      }
      spawn();
    }

    void initList() {
      initCity();
      initCave();
      for(int i=0;i<spawnList.Count;i++) {
        ammoList.Add(Instantiate(prefabList[i], spawnList[i],  Quaternion.identity) as GameObject);
        timerList.Add(delay);
        isSpawningList.Add(false);
      }
    }

    void timer(int index) {
      if(ammoList[index]==null) {
        timerList[index] -= Time.deltaTime;
      }
      if(timerList[index]<0) {
        timerList[index] = delay;
        isSpawningList[index] = true;
      }
      else {
        isSpawningList[index] = false;
      }
    }

    void spawn() {
      for(int i=0;i<ammoList.Count;i++) {
        if(isSpawningList[i]) {
          ammoList[i] = Instantiate(prefabList[i], spawnList[i],  Quaternion.identity) as GameObject;
        }
      }
    }

    void initCity() {
      spawnList.Add(new Vector3(-15.5f, 10f, 0f));
      spawnList.Add(new Vector3(-2.5f, 1.5f, 0f));
      spawnList.Add(new Vector3(1.5f, 1.5f, 0f));
      spawnList.Add(new Vector3(14.5f, 10f, 0f));
      for(int i=0;i<4;i++) {
        prefabList.Add(gunAmmoPrefab);
      }
    }

    void initCave() {
      spawnList.Add(new Vector3(-8.25f, -5f, 0f));
      spawnList.Add(new Vector3(8.25f, -5f, 0f));
      spawnList.Add(new Vector3(20.5f, -7f, 0f));
      spawnList.Add(new Vector3(-20.5f, -7f, 0f));
      for(int i=0;i<4;i++) {
        prefabList.Add(blasterAmmoPrefab);
      }
    }
}
