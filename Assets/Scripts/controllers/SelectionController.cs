using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour {

    private GameObject previousSelection;
    private Camera cam;

	// Use this for initialization
	void Start () {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) { 
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                Debug.Log("we got a hit...");
                if (previousSelection != null) {
                    previousSelection.GetComponent<SelectionHandler>().unselect();
                    previousSelection = null;
                }
                Debug.Log(hit.transform.gameObject);
                SelectionHandler handler = hit.transform.gameObject.GetComponent<SelectionHandler>();
                if (handler != null) {
                    Debug.Log("Trying to select");
                    previousSelection = hit.transform.gameObject;
                    handler.select();
                } else {
                    Debug.Log("but there was no handler...");
                }
            } else {
                if (previousSelection != null) {
                    previousSelection.GetComponent<SelectionHandler>().unselect();
                    previousSelection = null;
                }
            }
        }
    }
}
