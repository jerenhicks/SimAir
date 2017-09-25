using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance = null;
    private List<Airport> airports = null;
    private TimeEventListener listener = null;
    public GameObject airportPrefab;
    public GameObject airplanePrefab;
    private Dictionary<Airport, GameObject> airportToGameObjectMap;
    private Dictionary<Airplane, GameObject> airplaneToGameObjectMap;
    private int numAirports = 3;
    private List<Schedule> schedules = null;
    private List<Airplane> airplanes = null;
    private Dictionary<Airplane, Schedule> openAir = null;

    // Use this for initialization
    void Start () {
        airports = new List<Airport>();
        schedules = new List<Schedule>();
        airplanes = new List<Airplane>();
        openAir = new Dictionary<Airplane, Schedule>();
        TimeController.instance.handler += handleTimeEvent;
        airplaneToGameObjectMap = new Dictionary<Airplane, GameObject>();
        airportToGameObjectMap = new Dictionary<Airport, GameObject>();
        this.setupNewGame();
    }

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void setupNewGame() {
        HexMap_Continent.instance.generateMap();
        this.setupAirports();
        //make a dummy airplane
        Airplane airplane = new Airplane("AL130", "Beech", 10, 10, .5f);
        airplanes.Add(airplane);
        //make a dummy schedule
        Schedule schedule = new Schedule(airports[0], airports[1], airplanes[0], new InGameTime(0, 5));
        schedules.Add(schedule);
    }

    public void setupLoadGame() {
        //FIXME: should load things correctly, right now calls new game.
        this.setupNewGame();
    }

    public void Update() {
        if (TimeController.instance.isPlaying()) {
            List<Airplane> toRemove = new List<Airplane>();
            foreach (Airplane airplane in openAir.Keys) {
                float speed = airplane.getSpeed();
                float step = speed * Time.deltaTime / TimeController.instance.getCurrentSpeedValue();
                airplaneToGameObjectMap[airplane].transform.position = Vector3.MoveTowards(airplaneToGameObjectMap[airplane].transform.position, openAir[airplane].getDestination().getPosition(), step);
                airplaneToGameObjectMap[airplane].transform.LookAt(openAir[airplane].getDestination().getPosition());

                if (airplaneToGameObjectMap[airplane].transform.position == openAir[airplane].getDestination().getPosition()) {
                    toRemove.Add(airplane);
                }
            }
            foreach (Airplane airplane in toRemove) {
                Destroy(airplaneToGameObjectMap[airplane]);
                airplaneToGameObjectMap.Remove(airplane);
                //TODO: should add the airplane to the destination airport...
                openAir.Remove(airplane);
            }
        }
    }

    private void setupAirports() {
        for (int i = 0; i < numAirports; i++) {
            Hex hex = HexMap_Continent.instance.getRandomValidHex();
            Airport a1 = new Airport("G Municapal", 1, hex.getPosition());
            airports.Add(a1);
            GameObject airportGameObject = (GameObject)Instantiate(airportPrefab, hex.getPosition(), Quaternion.identity, this.transform);
            airportToGameObjectMap.Add(a1, airportGameObject);
        }
    }
    private void handleTimeEvent(object sender, EventArgs e) {
        foreach (Schedule schedule in schedules) {
            if (InGameTime.isTimesEqual(schedule.getDepartureTime(), TimeController.instance.getTime())) {
                GameObject airplaneGameObject = (GameObject)Instantiate(airplanePrefab, schedule.getDeparture().getPosition(), Quaternion.identity, this.transform);
                airplaneToGameObjectMap.Add(schedule.getAirplane(), airplaneGameObject);
                openAir.Add(schedule.getAirplane(), schedule);
            }
        }
    }
}
