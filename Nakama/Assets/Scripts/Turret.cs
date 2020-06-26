using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
  public List<GameObject> buttonList;
  Weapon weapon;
  public float fireRate = 20f;
  public bool activated = true;
  float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
      weapon = GetComponent<Weapon>();
      weapon.isShooting = activated;
    }

    // Update is called once per frame
    void Update()
    {
      shooting();
    }

    void shooting() {
      bool isShooting = activated;
      foreach(GameObject button in buttonList) {
        Button buttonScript = button.GetComponent<Button>();
        isShooting = isShooting ^ buttonScript.isOn;
      }
      if (isShooting && timer<=0) {
        weapon.addAmmo(1);
        weapon.shoot();
        timer = fireRate;
      }
      else {
        weapon.isShooting = false;
        timer = timer - Time.deltaTime;
      }
    }
}
