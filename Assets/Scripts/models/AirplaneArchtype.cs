using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneArchtype  {

    private string modelName;
    private int maxPassengers;
    private float speed;
    private float cost;
    private float fuelSize;

    public AirplaneArchtype(string modelName, int maxPassengers, float speed, float cost, float fuelSize) {
        this.modelName = modelName;
        this.maxPassengers = maxPassengers;
        this.speed = speed;
        this.cost = cost;
        this.fuelSize = fuelSize;
    }

    public string getModelName() {
        return this.modelName;
    }

    public int getMaxPassengers() {
        return this.maxPassengers;
    }

    public float getSpeed() {
        return this.speed;
    }

    public float getCost() {
        return this.cost;
    }
	
    public float getFuelSize() {
        return this.fuelSize;
    }
}
