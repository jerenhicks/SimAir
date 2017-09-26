using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScheduleManagerButtonHandler : MonoBehaviour {
    Button myButton;

    void Awake() {
        myButton = GetComponent<Button>(); // <-- you get access to the button component here

        myButton.onClick.AddListener(() => { onClick(); });
    }

    public void onClick() {
        ModalCanvasHandler handler = GameObject.Find("ScheduleManagerCanvas").GetComponent<ModalCanvasHandler>();
        handler.showPanel();
    }
}
