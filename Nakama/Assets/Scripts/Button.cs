using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

  public List<GameObject> buttonOnList;
  public List<GameObject> buttonOffList;
  public Sprite onSprite;
  Sprite offSprite;
  SpriteRenderer spriteRenderer;
  AudioSource audioSource;
  public bool isOn = false;
  Player player = null;

    // Start is called before the first frame update
    void Start()
    {
      audioSource = GetComponent<AudioSource>();
      spriteRenderer = GetComponent<SpriteRenderer>();
      offSprite = spriteRenderer.sprite;
    }

    // Update is called once per frame
    void Update()
    {
      switchButton();
      updateSprite();
    }

    void OnTriggerEnter2D(Collider2D col) {
      if (col.gameObject.tag == "Player1" || col.gameObject.tag == "Player2") {
        player = col.gameObject.GetComponent<Player>();
      }
    }

    void OnTriggerExit2D(Collider2D col) {
      if (col.gameObject.tag == "Player1" || col.gameObject.tag == "Player2") {
        player = null;
      }
    }

    void switchButton() {
      if (player!=null && player.isActing) {
        audioSource.Play();
        isOn = !isOn;
        switchOthersButton();
      }
    }

    void switchOthersButton() {
      foreach(GameObject buttonObject in buttonOnList) {
        Button button = buttonObject.GetComponent<Button>();
        button.isOn = true;
      }
      foreach(GameObject buttonObject in buttonOffList) {
        Button button = buttonObject.GetComponent<Button>();
        button.isOn = false;
      }
    }

    void updateSprite() {
      spriteRenderer.sprite = isOn ? onSprite : offSprite;
    }

}
