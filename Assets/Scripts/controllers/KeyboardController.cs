using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour {

    public static KeyboardController instance = null;

    // Use this for initialization
    void Start () {
		
	}

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ControlPanelCanvasHandler handler = GameObject.Find("ControlPanelCanvas").GetComponent<ControlPanelCanvasHandler>();
            handler.togglePanel();
        }
    }
}
