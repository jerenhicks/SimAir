using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameButtonHandler : MonoBehaviour {
    Button myButton;

    void Awake() {
        myButton = GetComponent<Button>(); // <-- you get access to the button component here

        myButton.onClick.AddListener(() => { onClick(); });
    }

    public void onClick() {
        PlayerController.instance.loadGame();
        SceneManager.LoadScene("game");
    }
}
