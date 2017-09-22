using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static PlayerController instance = null;
    private bool playing = false;
    private string airlineName = "Untitled";
    private double cash = 0.0;

    // Use this for initialization
    void Start() {
        Debug.Log("hi there, timecontroller start");
        DontDestroyOnLoad(gameObject);
    }

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public double getCash() {
        return this.cash;
    }

    public string getAirlineName() {
        return this.airlineName;
    }

    public void newGame(string airlineName, double cash) {
        this.airlineName = airlineName;
        this.cash = cash;
    }

    public void loadGame() {
        this.airlineName = "Quazi-Airline";
        this.cash = 10000.0;
    }
}
