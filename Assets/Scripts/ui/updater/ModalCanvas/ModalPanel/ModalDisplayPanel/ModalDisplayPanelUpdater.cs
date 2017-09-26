using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalDisplayPanelUpdater : MonoBehaviour {
    public GameObject individualPrefab;


    public virtual void showData() {
        //no-op
        Debug.Log("No data...");
    }

    public virtual void destroyData() {
        //no-op
    }
}
