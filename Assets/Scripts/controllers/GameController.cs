using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance = null;
    private List<Airport> airports = null;
    private TimeEventListener listener = null;
    public GameObject airportPrefab;
    private Dictionary<Airport, GameObject> airportToGameObjectMap;


    // Use this for initialization
    void Start () {
        airports = new List<Airport>();
        TimeController.instance.handler += test;
        airportToGameObjectMap = new Dictionary<Airport, GameObject>();
        //HexMap_Continent.instance.generateMap();
        this.setupNewGame();
    }

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void setupNewGame() {
        Airport a1 = new Airport("G Municapal", 1);
        airports.Add(a1);
        Airport a2 = new Airport("D International", 3);
        airports.Add(a2);
        Airport a3 = new Airport("Z Regional", 2);
        airports.Add(a3);

        HexMap_Continent.instance.generateMap();
        this.setupAirports();
    }

    public void setupLoadGame() {
        //FIXME: should load things correctly, right now calls new game.
        this.setupNewGame();
    }

    private void setupAirports() {
        foreach (Airport airport in airports) {
            Hex hex = HexMap_Continent.instance.getRandomValidHex();
            GameObject airportGameObject = (GameObject)Instantiate(airportPrefab, hex.getPosition(), Quaternion.identity, this.transform);
            airportToGameObjectMap.Add(airport, airportGameObject);
        }
    }
    private void test(object sender, EventArgs e) {
        Debug.Log("Event handler hit in game controller");
    }
}
