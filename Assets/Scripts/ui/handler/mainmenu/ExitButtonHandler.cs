using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitButtonHandler : MonoBehaviour {

    Button myButton;

    void Awake() {
        myButton = GetComponent<Button>(); // <-- you get access to the button component here

        myButton.onClick.AddListener(() => { onClick(); });
    }

    public void onClick() {
        Debug.Log("Trying to exit..");
        Application.Quit();
    }
}
