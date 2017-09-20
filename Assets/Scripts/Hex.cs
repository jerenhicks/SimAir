using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex {

    //q + r + s = 0;
    //S = -(Q + R)
    /** column.*/
    public readonly int Q;
    /** row.*/
    public readonly int R;
    public readonly int S;
    static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;
    float radius = .5f;
    bool allowWrapEastWest = true;
    bool allowWrapNorthSouth = false;

    public Hex(int q, int r) {
        this.Q = q;
        this.R = r;
        this.S = -(q + r);
    }

    public Vector3 getPosition() {
        return new Vector3(getHexHorizontalSpacing() * (this.Q + this.R / 2f), 0, getHexVerticalSpacing() * this.R);
    }

    public float getHexHeight() {
        return radius * 2;
    }

    public float getHexWidth() {
        return WIDTH_MULTIPLIER * getHexHeight();
    }

    public float getHexVerticalSpacing() {
        return getHexHeight() * .75f;
    }

    public float getHexHorizontalSpacing() {
        return getHexWidth();
    }

    public Vector3 getPositionFromCamera(Vector3 cameraPosition, float numRows, float numColumns) {
        float mapHeight = numRows * getHexVerticalSpacing();
        float mapWidth = numColumns * getHexHorizontalSpacing();

        Vector3 position = getPosition();

        if (allowWrapEastWest) {

            float howManyWidthsFromCamera = (position.x - cameraPosition.x) / mapWidth;

            if (howManyWidthsFromCamera > 0) {
                howManyWidthsFromCamera += 0.5f;
            }
            else {
                howManyWidthsFromCamera -= 0.5f;
            }

            int howManyWidthToFix = (int)howManyWidthsFromCamera;

            position.x -= howManyWidthToFix * mapWidth;
        } 

        if (allowWrapNorthSouth) {
            float howManyHeightsFromCamera = (position.z - cameraPosition.z) / mapHeight;

            if (howManyHeightsFromCamera > 0) {
                howManyHeightsFromCamera += 0.5f;
            }
            else {
                howManyHeightsFromCamera -= 0.5f;
            }

            int howManyHeightToFix = (int)howManyHeightsFromCamera;

            position.z -= howManyHeightToFix * mapHeight;
        }

        return position;
    }
}
