using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class VeryFastButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    Button myButton;
    Text myText;
    Color selected = Color.green;
    Color original = Color.black;
    Color hover = Color.red;
    bool hovering = false;

    void Awake() {
        myButton = GetComponent<Button>(); // <-- you get access to the button component here

        myButton.onClick.AddListener(() => { onClick(); });
        myText = myButton.GetComponentsInChildren<Text>()[0];
        myText.text = "|>>>";
    }

    void Update() {
        if (hovering) {
            myText.color = hover;
        }
        else if (TimeController.instance.isPlaying() && TimeController.instance.isVeryFastSpeed()) {
            myText.color = selected;
        }
        else {
            myText.color = original;
        }
    }

    public void onClick() {
        TimeController.instance.setVeryFastSpeed();
        TimeController.instance.play();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        hovering = false;
    }
}
