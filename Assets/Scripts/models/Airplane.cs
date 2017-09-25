using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane {

    private string name;
    private string type;
    private int maxSeats;
    private int fuelSize;
    private float speed;

    public Airplane(string name, string type, int maxSeats, int fuelSize, float speed) {
        this.name = name;
        this.type = type;
        this.maxSeats = maxSeats;
        this.fuelSize = fuelSize;
        this.speed = speed;
    }

    public string getName() {
        return this.name;
    }

    public string getType() {
        return this.type;
    }

    public int getMaxSeats() {
        return this.maxSeats;
    }

    public int getFuelSize() {
        return this.fuelSize;
    }

    public float getSpeed() {
        return this.speed;
    }
}
