using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activation : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> buttonList;
    Renderer spriteRenderer;
    BoxCollider2D boxCollider;
    public LogicGate logicGate = LogicGate.AND;
    public bool inverser = false;

    void Start()
    {
      spriteRenderer = GetComponent<Renderer>();
      boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
      activate();
    }

    void activate() {
      bool isActivated = true;
      switch(logicGate) {
        case LogicGate.AND:
          isActivated = andGate();
          break;
        case LogicGate.OR:
          isActivated = orGate();
          break;
        case LogicGate.XOR:
          isActivated = xorGate();
          break;
        default:
          break;
      }
      isActivated = isActivated ^ inverser;
      spriteRenderer.enabled = isActivated;
      boxCollider.enabled = isActivated;
    }

    bool andGate() {
      bool andActivating = true;
      foreach(GameObject buttonObjet in buttonList) {
        Button button = buttonObjet.GetComponent<Button>();
        andActivating = andActivating && button.isOn;
      }
      return andActivating;
    }

    bool orGate() {
      bool orActivating = false;
      foreach(GameObject buttonObjet in buttonList) {
        Button button = buttonObjet.GetComponent<Button>();
        orActivating = orActivating || button.isOn;
      }
      return orActivating;
    }

    bool xorGate() {
      bool xorActivating = false;
      foreach(GameObject buttonObjet in buttonList) {
        Button button = buttonObjet.GetComponent<Button>();
        xorActivating = xorActivating ^ button.isOn;
      }
      return xorActivating;
    }

    public enum LogicGate
    {
      XOR, OR, AND
    }
}
