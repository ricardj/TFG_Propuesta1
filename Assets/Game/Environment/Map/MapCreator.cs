using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using static MapDesign;
using System;

[Serializable]
public class Map
{
    public List<int> mapSize { get; set; }
    public List<List<string>> mapMatrix { get; set; }
}

[ExecuteInEditMode]
public class MapCreator : MonoBehaviour
{

    public TextAsset jsonMap;
    public GameObject cellMapPrefab;
    public GameObject mapHolder;
   
    public Vector3 cellSize = Vector3.zero;

    public GameObject terrain;
    Vector2 mapSize = Vector2.zero;
    public GameObject outpostPrefab;

    public GameObject woodsPrefab;

    public bool update;




    // Start is called before the first frame update
    void Start()
    {
        //mapHolder = GameObject.FindGameObjectWithTag("MapHolder");
        if(mapHolder == null)
        {
            Debug.LogWarning("Error: missing map holder.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (update)
        {
            generateTerrainMap();
            update = false;
        }
    }

    public GameObject[][] generateTerrainMap()
    {
        //We create the instance of map
        Map map;
        GameObject[][] mapMatrix;

        //We destroy all previous map
        GameObject[] mapCells = GameObject.FindGameObjectsWithTag("MapCell");
        foreach (GameObject mapCell in mapCells) DestroyImmediate(mapCell);

        GameObject[] woodPlaces = GameObject.FindGameObjectsWithTag("Woods");
        foreach (GameObject wood in woodPlaces) DestroyImmediate(wood);

        GameObject[] outpostPlaces = GameObject.FindGameObjectsWithTag("Outpost");
        foreach (GameObject outpostPlace in outpostPlaces) DestroyImmediate(outpostPlace);

        

        //We check if we have a json map assigned
        if (jsonMap != null)
        {
            //Debug.Log(jsonMap.text);

            map = JsonConvert.DeserializeObject<Map>(jsonMap.text);
            //map = JsonUtility.FromJson<Map>(jsonMap.text); 
            //Debug.Log(map);
            //Debug.Log(map.mapSize[0]);
            mapSize.x = map.mapSize[0];
            mapSize.y = map.mapSize[1];

            //Debug.Log(map.mapMatrix[0][0]);
        }
        else
        {
            Debug.LogWarning("Error: no json level assigned to the map.");
            return null;
        }

        mapMatrix = new GameObject[(int)mapSize.x][];
        for (int i = 0; i < mapSize.x; i++)
        {
            mapMatrix[i] = new GameObject[(int)mapSize.y];
            for (int j = 0; j < mapSize.y; j++)
            {
                Vector3 cellMapPosition = mapHolder.transform.position;
                cellMapPosition.x += cellSize.x / 2 + i * cellSize.x;
                cellMapPosition.z += cellSize.y / 2 + j * cellSize.y;

                mapMatrix[i][j] = Instantiate(cellMapPrefab, cellMapPosition, Quaternion.identity);

                if (jsonMap != null) mapMatrix[i][j].GetComponent<MapCell>().terrainType = MapCellTypeTraductor[map.mapMatrix[i][j]];

                cellMapPrefab.GetComponent<BoxCollider>().size = cellSize;

                //We make the map child of the map gameobject
                mapMatrix[i][j].transform.parent = mapHolder.transform;
            }
        }

        if(terrain != null)
        {
            TerrainManipulator terrainManipulator = terrain.GetComponent<TerrainManipulator>();
            terrainManipulator.TerrainGeneration(mapMatrix);
        }

        //We place the outposts wherever they go
        for (int i = 0; i < mapSize.x; i++)
        {
            for (int j = 0; j < mapSize.y; j++)
            {
                switch(mapMatrix[i][j].GetComponent<MapCell>().terrainType){
                    case MapCellType.Outpost:
                        Vector3 outpostPosition = mapMatrix[i][j].transform.position;
                        outpostPosition.y= 0;
                        Instantiate(outpostPrefab,outpostPosition, Quaternion.identity);
                        break;
                    case MapCellType.Woods:
                        Vector3 woodsPosition = mapMatrix[i][j].transform.position;
                        woodsPosition.y= 0;
                        Instantiate(woodsPrefab,woodsPosition, Quaternion.identity);
                        break;
                    default:
                        break;
                }
                
            }
        }



        return mapMatrix;
    }
}
