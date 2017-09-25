using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airport {

    private string name = "Untitle";
    private int size = 0;
    private Vector3 position;

	public Airport(string name, int size, Vector3 position) {
        this.name = name;
        this.size = size;
        this.position = position;
    }

    public string getName() {
        return this.name;
    }

    public int getSize() {
        return this.size;
    }

    public Vector3 getPosition() {
        return this.position;
    }

}
