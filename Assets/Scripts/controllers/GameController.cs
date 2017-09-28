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
    private List<AirplaneArchtype> archtypes = null;

    //not sure if we want to store the raw player data here or encapsulate it into an object
    private string airlineName;
    private float playerMoney;

    // Use this for initialization
    void Start () {
        //initialize variables
        airports = new List<Airport>();
        schedules = new List<Schedule>();
        airplanes = new List<Airplane>();
        openAir = new Dictionary<Airplane, Schedule>();
        airplaneToGameObjectMap = new Dictionary<Airplane, GameObject>();
        airportToGameObjectMap = new Dictionary<Airport, GameObject>();
        archtypes = new List<AirplaneArchtype>();

        TimeController.instance.handler += handleTimeEvent;
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
        this.setupAirplaneArchtypes();
        this.setupPlayer();
        //make a dummy airplane
        Airplane airplane = new Airplane("AL130", archtypes[0]);
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
                this.addPlayerMoney(10000f);
                Destroy(airplaneToGameObjectMap[airplane]);
                airplaneToGameObjectMap.Remove(airplane);
                //TODO: should add the airplane to the destination airport...
                openAir.Remove(airplane);
            }
        }
    }

    private void setupAirplaneArchtypes() {
        AirplaneArchtype type1 = new AirplaneArchtype("Penguin", 10, 3, 10000, 15);
        AirplaneArchtype type2 = new AirplaneArchtype("Stork", 50, 3, 10000, 30);
        AirplaneArchtype type3 = new AirplaneArchtype("Pelican", 100, 3, 10000, 100);
        AirplaneArchtype type4 = new AirplaneArchtype("Test", 100, 3, 10000, 100);
        AirplaneArchtype type5 = new AirplaneArchtype("Test", 100, 3, 10000, 100);
        AirplaneArchtype type6 = new AirplaneArchtype("Test", 100, 3, 10000, 100);
        archtypes.Add(type1);
        archtypes.Add(type2);
        archtypes.Add(type3);
        archtypes.Add(type4);
        archtypes.Add(type5);
        archtypes.Add(type6);
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
                airplaneGameObject.GetComponent<SelectionHandler>().setPlane();
                airplaneToGameObjectMap.Add(schedule.getAirplane(), airplaneGameObject);
                openAir.Add(schedule.getAirplane(), schedule);
            }
        }
    }
    private void setupPlayer() {
        this.airlineName = "Epsilon Airlines";
        this.playerMoney = 5000;
    }

    public List<AirplaneArchtype> getAirplaneArchtypes() {
        return this.archtypes;
    }

    public string getAirlineName() {
        return this.airlineName;
    }

    public float getPlayerMoney() {
        return this.playerMoney;
    }

    public void addPlayerMoney(float money) {
        this.playerMoney += money;
    }

    public void setPlayerMoney(float money) {
        this.playerMoney = money;
    }

    public bool purchaseAirplane(AirplaneArchtype type) {
        if (type.getCost() > this.playerMoney) {
            return false;
        }
        Debug.Log("Buying a " + type.getModelName());
        this.playerMoney -= type.getCost();
        Airplane airplane = new Airplane("DL130", type);
        airplanes.Add(airplane);

        return true;
    }

    public List<Schedule> getSchedules() {
        return this.schedules;
    }
}
