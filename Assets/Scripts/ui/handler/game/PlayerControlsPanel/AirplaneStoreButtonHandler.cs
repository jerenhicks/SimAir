﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AirplaneStoreButtonHandler : MonoBehaviour {

    Button myButton;

    void Awake() {
        myButton = GetComponent<Button>(); // <-- you get access to the button component here

        myButton.onClick.AddListener(() => { onClick(); });
    }

    public void onClick() {
        ModalCanvasHandler handler = GameObject.Find("AirplaneStoreCanvas").GetComponent<ModalCanvasHandler>();
        handler.showPanel();
    }
}
