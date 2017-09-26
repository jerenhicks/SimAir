using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseButtonHandler : MonoBehaviour {

    Button myButton;
    private AirplaneArchtype type;

    void Awake() {
        myButton = GetComponent<Button>(); // <-- you get access to the button component here

        myButton.onClick.AddListener(() => { onClick(); });
    }

    public void setType(AirplaneArchtype type) {
        this.type = type;
    }


    public void onClick() {
        Debug.Log("On click...");
        if (this.type != null) {
            Debug.Log("type was set, lets do this");
            GameController.instance.purchaseAirplane(type);
        }
    }
}
