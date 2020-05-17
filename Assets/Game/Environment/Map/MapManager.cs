using System;
using System.Collections.Generic;
using UnityEngine;
using static MapDesign;





public class MapManager : MonoBehaviour
{
    [HideInInspector]
    public Vector2 mapSize = Vector2.zero;



    [HideInInspector]
    public GameObject[][] mapMatrix; //Matrix of map cells
    


    // Start is called before the first frame update
    void Start()
    {
        MapCreator mapCreator = GetComponent<MapCreator>();
        mapMatrix = mapCreator.generateTerrainMap();
        mapSize = new Vector2(mapMatrix.Length, mapMatrix[0].Length);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public MapCellInfo[][] getPlayerSurroundingsMatrix(int xSize, int ySize)
    {
        //We query he player
        Vector2 playerPosition = whereIsPlayer();
        //Debug.Log("Player is here: " + playerPosition.ToString());

        //We build a MapCellInfo matrix of the requested size 
        MapCellInfo[][] mapCellInfo = new MapCellInfo[xSize][];
        for(int i = 0; i < xSize; i++)
        {
            mapCellInfo[i] = new MapCellInfo[ySize];
            for (int j = 0; j < ySize; j++)
            {
                int xValue = i  + (int)playerPosition.x -(int)Mathf.Floor(xSize/2) ;
                if (xValue>= mapSize.x || xValue < 0)
                {
                    mapCellInfo[i][j] = new MapCellInfo(MapCellType.Plain);
                }
                else
                {
                    int yValue = j + (int)playerPosition.y -(int) Mathf.Floor(ySize/2);
                    if (yValue >= ySize || yValue < 0)
                    {
                        mapCellInfo[i][j] = new MapCellInfo(MapCellType.Plain);
                    }
                    else
                    {
                        mapCellInfo[i][j] = new MapCellInfo(mapMatrix[xValue][yValue].GetComponent<MapCell>().terrainType);
                    }
                   
                }
            }  
        }

        return mapCellInfo;
    }

    public Vector2 whereIsPlayer()
    {
        Vector2 playerPosition = Vector2.zero;
        for (int i = 0; i < mapSize.x; i++)
        {
            for (int j = 0; j < mapSize.y; j++)
            {
                
                if (mapMatrix[i][j].GetComponent<MapCell>().containsPlayer)
                {
                    playerPosition.x = i;
                    playerPosition.y = j;
                    break;
                }
            }
        }

        return playerPosition;
    }


}
