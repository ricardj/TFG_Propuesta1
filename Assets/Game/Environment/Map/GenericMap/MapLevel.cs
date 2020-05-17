using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static MapDesign;


[Serializable]
public class MapLevel
{
    public MapCellType[][] mapMatrix;
    public Vector2 mapSize;

    public MapLevel(int x, int y)
    {

        mapSize.x = x;
        mapSize.y = y;
        mapMatrix = new MapCellType[x][];
        for (int i = 0; i < mapSize.x; i++)
        {
            mapMatrix[i] = new MapCellType[y];
            for (int j = 0; j < mapSize.y; j++)
            {
                mapMatrix[i][j] = MapCellType.Plain;
            }
        }

    }
}
