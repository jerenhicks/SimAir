using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneListPanelUpdater : MonoBehaviour {

    public GameObject airplaneArchtypePrefab;
    private List<GameObject> gameObjects;

    // Use this for initialization
    void Start () {
        gameObjects = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void showData() {
        int spacing = 175;
        foreach (AirplaneArchtype type in GameController.instance.getAirplaneArchtypes()) {
            GameObject airportGameObject = (GameObject)Instantiate(airplaneArchtypePrefab, this.transform.position, Quaternion.identity, this.transform);
            gameObjects.Add(airportGameObject);
            airportGameObject.GetComponent<IndividualPlanePanelUpdater>().setAirplaneArchtype(type);
            airportGameObject.transform.SetParent(this.transform);
            //RectTransform rectTransform = airportGameObject.GetComponent<RectTransform>();
            //rectTransform.anchoredPosition = new Vector2(0, spacing);
            //spacing -= 100;
        }
    }

    public void destroyData() {
        foreach (GameObject gameObject in gameObjects) {
            Destroy(gameObject);
        }
    }
}
