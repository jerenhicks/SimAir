using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class IndividualPlanePanelUpdater : MonoBehaviour {

    private AirplaneArchtype type;

    private void Update() {
        Transform child = transform.Find("PurchaseButton");
        Button button = child.GetComponent<Button>();
        Text text = button.GetComponentsInChildren<Text>()[0];
        if (GameController.instance.getPlayerMoney() < type.getCost()) {
            button.interactable = false;
            text.color = Color.grey;
        } else {
            button.interactable = true;
            text.color = Color.black;
        }
    }

    public void setAirplaneArchtype(AirplaneArchtype type) {
        this.type = type;
        Transform child = transform.Find("SpeedDisplayText");
        Text t = child.GetComponent<Text>();
        t.text = "" + type.getSpeed();

        child = transform.Find("PassengersDisplayText");
        t = child.GetComponent<Text>();
        t.text = "" + type.getMaxPassengers();

        child = transform.Find("FuelSizeDisplayText");
        t = child.GetComponent<Text>();
        t.text = "" + type.getFuelSize();

        child = transform.Find("AirplaneNameText");
        t = child.GetComponent<Text>();
        t.text = "" + type.getModelName();

        child = transform.Find("CostText");
        t = child.GetComponent<Text>();
        t.text = "" + type.getCost();

        child = transform.Find("PurchaseButton");
        PurchaseButtonHandler handler = child.GetComponent<PurchaseButtonHandler>();
        handler.setType(type);
    }
}
