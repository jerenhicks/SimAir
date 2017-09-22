using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanelCanvasHandler : MonoBehaviour {

    public GameObject panel;
	
    public void togglePanel() {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup.alpha == 0f) {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        } else {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
