﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalCanvasHandler : MonoBehaviour {

    public void togglePanel() {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup.alpha == 0f) {
            this.showPanel();
        } else {
            this.hidePanel();
        }
    }

    public void showPanel() {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        ModalDisplayPanelUpdater listUpdater = this.GetComponentInChildren<ModalDisplayPanelUpdater>();
        listUpdater.showData();
    }

    public void hidePanel() {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        ModalDisplayPanelUpdater listUpdater = this.GetComponentInChildren<ModalDisplayPanelUpdater>();
        listUpdater.destroyData();
    }
}
