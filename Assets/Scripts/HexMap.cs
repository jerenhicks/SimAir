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

    public Material MatSelected;

    private Hex[,] hexes;
    private Dictionary<Hex, GameObject> hexToGameObjectMap;
    public bool allowWrapEastWest = true;
    public bool allowWrapNorthSouth = false;

    public float heightMountain = 1f;
    public float heightHill = 0.6f;
    public float heightFlat = 0.0f;

    public List<Hex> flatHexes = null;

    // Use this for initialization
    void Start() {
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


                GameObject hexGo = (GameObject)Instantiate(HexPrefab, h.getPosition(), Quaternion.identity, this.transform);
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
                } else if (h.elevation >= heightFlat) {
                    mr.material = MatPlains;
                    mf.mesh = MeshFlat;
                } else {
                    mr.material = MatOcean;
                    mf.mesh = MeshWater;
                }


            }
        }
    }

    //FIXME: because of the switch to an offset grid, this method does not calculate things correctly
    //public Hex[] getHexesWithinRangeOf(Hex centerHex, int range) {
    //    List<Hex> results = new List<Hex>();
    //    for (int dx = -range; dx < range - 1; dx++) {
    //        for (int dy = Mathf.Max(-range + 1, -dx - range); dy < Mathf.Min(range, -dx + range - 1); dy++) {
    //            results.Add(getHexAt(centerHex.Q + dx, centerHex.R + dy));
    //        }
    //    }
    //    return results.ToArray();
    //}


    public List<Hex> testMethodForFindingNeighbors(Hex centerHex) {
        List<Hex> results = new List<Hex>();
        //results.Add(centerHex);

        List<Hex> neighbors = new List<Hex>();
        if (centerHex.R % 2 ==0) {
            //bottom left 
            int leftX = centerHex.Q - 1;
            int leftY = centerHex.R - 1;
            if (leftX >= 0 && leftY >= 0) {
                neighbors.Add(getHexAt(leftX, leftY));
            }

            //bottom right
            int rightX = centerHex.Q;
            int rightY = centerHex.R - 1;
            if (rightX >= 0 && rightY >= 0) {
                neighbors.Add(getHexAt(rightX, rightY));
            }

            //top left
            leftX = centerHex.Q - 1;
            leftY = centerHex.R + 1;
            if (leftX >= 0 && leftY >= 0) {
                neighbors.Add(getHexAt(leftX, leftY));
            }

            //bottom right
            rightX = centerHex.Q;
            rightY = centerHex.R + 1;
            if (rightX >= 0 && rightY >= 0) {
                neighbors.Add(getHexAt(rightX, rightY));
            }

            //left side
            leftX = centerHex.Q - 1;
            leftY = centerHex.R;
            if (leftX >= 0 || leftY >= 0) {
                neighbors.Add(getHexAt(leftX, leftY));
            }

            //right side
            leftX = centerHex.Q + 1;
            leftY = centerHex.R;
            if (leftX >= 0 && leftY >= 0) {
                neighbors.Add(getHexAt(leftX, leftY));
            }

        } else {
            //bottom left
            int leftX = centerHex.Q;
            int leftY = centerHex.R - 1;
            if (leftX >= 0 && leftY >= 0) {
                neighbors.Add(getHexAt(leftX, leftY));
            }

            //bottom right
            int rightX = centerHex.Q + 1;
            int rightY = centerHex.R - 1;
            if (rightX >= 0 && rightY >= 0) {
                neighbors.Add(getHexAt(rightX, rightY));
            }

            //top left
            leftX = centerHex.Q;
            leftY = centerHex.R + 1;
            if (leftX >= 0 && leftY >= 0) {
                neighbors.Add(getHexAt(leftX, leftY));
            }

            //top right
            rightX = centerHex.Q + 1;
            rightY = centerHex.R + 1;
            if (rightX >= 0 && rightY >= 0) {
                neighbors.Add(getHexAt(rightX, rightY));
            }

            //left side
            leftX = centerHex.Q - 1;
            leftY = centerHex.R;
            if (leftX >= 0 && leftY >= 0) {
                neighbors.Add(getHexAt(leftX, leftY));
            }

            //right side
            rightX = centerHex.Q + 1;
            rightY = centerHex.R;
            if (rightX >= 0 && rightY >= 0) {
                neighbors.Add(getHexAt(rightX, rightY));
            }
        }
        return neighbors;
    }

    //public void selecNeighbors(Hex centerHex) {
    //    List<Hex> hexes = this.testMethodForFindingNeighbors(centerHex);
    //    foreach (Hex hex in hexes) {
    //        GameObject hexGo = hexToGameObjectMap[hex];
    //        MeshRenderer mr = hexGo.GetComponentInChildren<MeshRenderer>();
    //        mr.material = MatSelected;
    //    }
    //}

    public Hex[] getHexesWithinRangeOf(Hex centerHex, int radius) {
        HashSet<Hex> master = new HashSet<Hex>();
        master.Add(centerHex);
        HashSet<Hex> previous = master;
        
        for (int i = 0; i < radius; i++) {
            HashSet<Hex> newPrevious = new HashSet<Hex>();
            foreach (Hex hex in previous) {
                foreach(Hex neighbor in testMethodForFindingNeighbors(hex)) {
                    newPrevious.Add(neighbor);
                }
            }
            previous = newPrevious;
            foreach (Hex hex in master) {
                previous.Remove(hex);
            }
            foreach (Hex hex in previous) {
                master.Add(hex);
            }
        }

        Hex[] copy = new Hex[master.Count];
        master.CopyTo(copy);
        return copy;
    }

    public Hex getRandomValidHex() {
        int i = Random.Range(0, flatHexes.Count);
        return flatHexes[i];
    }
}
