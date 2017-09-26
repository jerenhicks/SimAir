using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseModalButtonHandler : MonoBehaviour {
    public GameObject canvas;
    Button myButton;

    void Awake() {
        myButton = GetComponent<Button>(); // <-- you get access to the button component here

        myButton.onClick.AddListener(() => { onClick(); });
    }

    public void onClick() {
        ModalCanvasHandler handler = canvas.GetComponent<ModalCanvasHandler>();
        handler.hidePanel();
    }
}
