using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour {

    public readonly int numRows = 30;
    public readonly int numCols = 60;

    public GameObject HexPrefab;

    public Mesh MeshWater;
    public Mesh MeshFlat;
    public Mesh MeshHill;
    public Mesh MeshMountain;

    public Material MatOcean;
    public Material MatPlains;
    public Material MatGrassLands;
    public Material MatMountains;


    // Use this for initialization
    void Start () {
        generateMap();
	}
	
	virtual public void generateMap() {
        for (int column = 0; column < numCols; column++) {
            for (int row = 0; row < numRows; row++) {
                Hex h = new Hex(column, row);
                //instantiate a hex

                Vector3 pos = h.getPositionFromCamera(Camera.main.transform.position, numRows, numCols);

                GameObject hexGo = (GameObject) Instantiate(HexPrefab, pos, Quaternion.identity, this.transform);

                hexGo.GetComponent<HexComponent>().hex = h;
                hexGo.GetComponent<HexComponent>().hexMap = this;

                hexGo.GetComponentInChildren<TextMesh>().text = string.Format("{0},{1}", column, row);

                MeshRenderer mr = hexGo.GetComponentInChildren<MeshRenderer>();
                mr.material = MatOcean;

                MeshFilter mf = hexGo.GetComponentInChildren<MeshFilter>();
                mf.mesh = MeshWater;
            }
        }
    }


}
