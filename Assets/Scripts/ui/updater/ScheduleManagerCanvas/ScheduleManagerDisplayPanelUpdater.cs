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
        foreach (Schedule schedule in GameController.instance.getSchedules()) {
            GameObject scheduleGameObject = (GameObject)Instantiate(this.individualPrefab, this.transform.position, Quaternion.identity, this.transform);
            gameObjects.Add(scheduleGameObject);
            //airportGameObject.GetComponent<IndividualPlanePanelUpdater>().setAirplaneArchtype(type);
            scheduleGameObject.transform.SetParent(this.transform);
        }
    }

    public override void destroyData() {
        foreach (GameObject gameObject in gameObjects) {
            Destroy(gameObject);
        }
    }
}
