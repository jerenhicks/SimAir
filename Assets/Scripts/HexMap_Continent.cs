using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap_Continent : HexMap {

	override public void generateMap() {
        base.generateMap();

        //debug
        Random.InitState(0);

        int numContinents = 2;
        int continentSpacing = 20;
        for (int c = 0; c < numContinents; c++) {
            int numSplats = Random.Range(4, 8);
            for (int i = 0; i < numSplats; i++) {
                int range = Random.Range(5, 8);
                int y = Random.Range(range, numRows - range);
                int x = Random.Range(0, 10) - y / 2 + (c * continentSpacing);

                elevateArea(x, y, range);
            }
        }

        float noiseResolution = 0.1f;
        Vector2 noiseOffset = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
        float noiseScale = 2f;
        for (int column = 0; column < numCols; column++) {
            for (int row = 0; row < numRows; row++) {
                Hex h = getHexAt(column, row);
                float n = Mathf.PerlinNoise(((float)column/ Mathf.Max(numCols, numRows) / noiseResolution) + noiseOffset.x, ((float) row/Mathf.Max(numCols, numRows) / noiseResolution) + noiseOffset.y) - 0.5f;
                h.elevation += n * noiseScale;
            }
        }

        updateHexVisuals();
    }

    void elevateArea(int q, int r, int range, float centerHeight = .8f) {
        Hex centerHex = getHexAt(q, r);
        centerHex.elevation = 0.5f;

        Hex[] areaHexes = getHexesWithinRangeOf(centerHex, range);
        foreach(Hex h in areaHexes) {
            h.elevation = centerHeight * Mathf.Lerp(1f, 0.25f, Mathf.Pow(Hex.getDistance(centerHex, h) / range, 2f));
        }
    }
}
