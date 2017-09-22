using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameButtonHandler : MonoBehaviour {

    Button myButton;

    void Awake() {
        myButton = GetComponent<Button>(); // <-- you get access to the button component here

        myButton.onClick.AddListener(() => { onClick(); });
    }

    public void onClick() {
        Debug.Log("New Game Clicked");
        PlayerController.instance.newGame("J-Air", 10000.0);
        SceneManager.LoadScene("game");
    }
}
