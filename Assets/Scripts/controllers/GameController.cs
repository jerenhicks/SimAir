using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance = null;
    private List<Airport> airports = null;
    private TimeEventListener listener = null;

    // Use this for initialization
    void Start () {
        airports = new List<Airport>();
        TimeController.instance.handler += test;
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

    }

    public void setupLoadGame() {
        //FIXME: should load things correctly, right now calls new game.
        this.setupNewGame();
    }

    private void test(object sender, EventArgs e) {
        Debug.Log("Event handler hit in game controller");
    }


    
}
