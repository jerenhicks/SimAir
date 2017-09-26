using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane {

    private string name;
    private AirplaneArchtype type;

    public Airplane(string name, AirplaneArchtype type) {
        this.name = name;
        this.type = type;
    }

    public string getName() {
        return this.name;
    }

    public AirplaneArchtype getType() {
        return this.type;
    }

    public string getModel() {
        return this.type.getModelName();
    }

    public int getMaxSeats() {
        return this.type.getMaxPassengers();
    }

    public float getFuelSize() {
        return this.type.getFuelSize();
    }

    public float getSpeed() {
        return this.type.getFuelSize();
    }
}
