using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlasmaHUD : MonoBehaviour
{
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
      slider = GetComponent<Slider>();
      slider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
      updatePlasma();
    }

    void updatePlasma() {
      GameObject player = GameObject.FindWithTag("Player2");
      if(player!=null) {
        Weapon weapon = player.GetComponent<Weapon>();
        slider.value = weapon.nbBullet;
      }
    }
}
