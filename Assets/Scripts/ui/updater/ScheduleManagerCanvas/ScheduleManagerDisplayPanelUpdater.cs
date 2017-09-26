using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScheduleManagerDisplayPanelUpdater : ModalDisplayPanelUpdater {

    private List<GameObject> gameObjects;

    // Use this for initialization
    void Start() {
        gameObjects = new List<GameObject>();
    }

    public override void showData() {
        foreach (AirplaneArchtype type in GameController.instance.getAirplaneArchtypes()) {
            GameObject airportGameObject = (GameObject)Instantiate(this.individualPrefab, this.transform.position, Quaternion.identity, this.transform);
            gameObjects.Add(airportGameObject);
            airportGameObject.GetComponent<IndividualPlanePanelUpdater>().setAirplaneArchtype(type);
            airportGameObject.transform.SetParent(this.transform);
        }
    }

    public override void destroyData() {
        foreach (GameObject gameObject in gameObjects) {
            Destroy(gameObject);
        }
    }
}
