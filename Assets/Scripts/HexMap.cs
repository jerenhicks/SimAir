using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour {


    public static HexMap instance = null;
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

    private Hex[,] hexes;
    private Dictionary<Hex, GameObject> hexToGameObjectMap;
    public bool allowWrapEastWest = true;
    public bool allowWrapNorthSouth = false;

    public float heightMountain = 1f;
    public float heightHill = 0.6f;
    public float heightFlat = 0.0f;

    public List<Hex> flatHexes = null;

    // Use this for initialization
    void Start () {
        flatHexes = new List<Hex>();
        //generateMap();
	}

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public Hex getHexAt(int x, int y) {
        if (hexes == null) {
            Debug.LogError("Hexes array not yet instantiated");
            return null;
        }

        if (allowWrapEastWest) {
            x = x % numCols;
            if (x < 0) {
                x += numCols;
            }
        }
        if (allowWrapNorthSouth) {
            y = y % numRows;
            if (y < 0) {
                Debug.Log("Y was..." + y + "will be: " + y + numRows);
                y += numRows;
            }
        }
        try {
            return hexes[x, y];
        } catch {
            Debug.LogError("Error: " + x + "," + y);
            return null;
        }
    }
	
	virtual public void generateMap() {
        hexes = new Hex[numCols, numRows];
        hexToGameObjectMap = new Dictionary<Hex, GameObject>();

        for (int column = 0; column < numCols; column++) {
            for (int row = 0; row < numRows; row++) {
                Hex h = new Hex(this, column, row);
                h.elevation = -0.5f;
                hexes[column, row] = h;


                GameObject hexGo = (GameObject) Instantiate(HexPrefab, h.getPosition(), Quaternion.identity, this.transform);
                hexToGameObjectMap.Add(h, hexGo);

                hexGo.GetComponent<HexComponent>().hex = h;
                hexGo.GetComponent<HexComponent>().hexMap = this;

                hexGo.GetComponentInChildren<TextMesh>().text = string.Format("{0},{1}", column, row);
            }
        }

        updateHexVisuals();
    }

    public void updateHexVisuals() {
        for (int column = 0; column < numCols; column++) {
            for (int row = 0; row < numRows; row++) {
                Hex h = hexes[column, row];
                GameObject hexGo = hexToGameObjectMap[h];

                MeshRenderer mr = hexGo.GetComponentInChildren<MeshRenderer>();
                MeshFilter mf = hexGo.GetComponentInChildren<MeshFilter>();
                if (h.elevation >= heightMountain) {
                    mr.material = MatMountains;
                    mf.mesh = MeshMountain;
                } else if (h.elevation >= heightHill) {
                    mr.material = MatGrassLands;
                    mf.mesh = MeshHill;
                }
                else if (h.elevation >= heightFlat) {
                    mr.material = MatPlains;
                    mf.mesh = MeshFlat;
                }
                else {
                    mr.material = MatOcean;
                    mf.mesh = MeshWater;
                }


            }
        }
    }

    //FIXME: because of the switch to an offset grid, this method does not calculate things correctly
    public Hex[] getHexesWithinRangeOf(Hex centerHex, int range) {
        List<Hex> results = new List<Hex>();
        for (int dx = -range; dx < range - 1; dx++) {
            for (int dy = Mathf.Max(-range + 1, -dx - range); dy < Mathf.Min(range, -dx + range - 1); dy++) {
                results.Add(getHexAt(centerHex.Q + dx, centerHex.R + dy));
            }
        }
        return results.ToArray();
    }

    public void testMethodForFindingNeighbors(Hex centerHex, int range) {
        List<Hex> results = new List<Hex>();
        for (int r = 0; r < range; r++) {

        }
    }

    public Hex getRandomValidHex() {
        int i = Random.Range(0, flatHexes.Count);
        return flatHexes[i];
    }
}
