using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class CashTextUpdater : MonoBehaviour {

    Text myText;

    // Use this for initialization
    void Start() {
        myText = GetComponent<Text>();

        if (myText == null) {
            Debug.LogError("DateTextUPdater: No 'Text' UI component on this object.");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update() {
        myText.text = String.Format("Cash: {0:C}", PlayerController.instance.getCash());
    }
}
