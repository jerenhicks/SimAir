using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotion : MonoBehaviour {

    Vector3 oldPosition;
    HexComponent[] hexes;

    // Use this for initialization
    void Start () {
        oldPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {


        checkIfCameraMoved();

	}

    public void PanToHex(Hex hex) {

    }

    void checkIfCameraMoved() {
        if (oldPosition != this.transform.position) {
            oldPosition = this.transform.position;

            if (hexes == null) {
                hexes = GameObject.FindObjectsOfType<HexComponent>();
            }
            foreach(HexComponent hex in hexes) {
                hex.updatePosition();
            }
        }
    }
}
