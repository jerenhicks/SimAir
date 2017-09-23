using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger : MonoBehaviour {

    private string name = "Untitled";
    private float money = 0.0f;
    private Airport destination = null;

    public Passenger(string name, float money, Airport destination) {
        this.name = name;
        this.money = money;
        this.destination = destination;
    }

    public string getName() {
        return this.name;
    }

    public float getMoney() {
        return this.money;
    }

    public Airport getDestination() {
        return this.destination;
    }

}
