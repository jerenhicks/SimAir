using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHandler : MonoBehaviour {

    public GameObject selectionPrefab;
    private GameObject gameObject;
    private bool plane = false;

    public void setPlane() {
        this.plane = true;
    }

    public void select() {
        gameObject = (GameObject)Instantiate(selectionPrefab, this.transform.position, Quaternion.Euler(new Vector3(90, 0, 0)), this.transform);
        if (plane) {
            gameObject.transform.localScale += new Vector3(4, 4, 4);
        }
    }

    public void unselect() {
        Destroy(gameObject);
    }
}
