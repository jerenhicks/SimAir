using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CloseAirplaneStoreButtonHandler : MonoBehaviour {

    Button myButton;

    void Awake() {
        myButton = GetComponent<Button>(); // <-- you get access to the button component here

        myButton.onClick.AddListener(() => { onClick(); });
    }

    public void onClick() {
        AirplaneStoreCanvasHandler handler = GameObject.Find("AirplaneStoreCanvas").GetComponent<AirplaneStoreCanvasHandler>();
        handler.hidePanel();
    }
}
