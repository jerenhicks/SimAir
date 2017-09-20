﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexComponent : MonoBehaviour {
    public Hex hex;
    public HexMap hexMap;

    public void updatePosition() {
        this.transform.position = hex.getPositionFromCamera(Camera.main.transform.position, hexMap.numRows, hexMap.numCols);
    }
}
