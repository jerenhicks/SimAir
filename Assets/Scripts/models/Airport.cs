using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airport {

    private string name = "Untitle";
    private int size = 0;

	public Airport(string name, int size) {
        this.name = name;
        this.size = size;
    }

    public string getName() {
        return this.name;
    }

    public int getSize() {
        return this.size;
    }
}
