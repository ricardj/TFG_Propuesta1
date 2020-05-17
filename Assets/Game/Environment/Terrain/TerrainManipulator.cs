using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MapDesign;

public class TerrainManipulator : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Vector2 cellSize = new Vector2(50,50);
    
    public int xSize = 10;
    public int ySize = 10;

    public int cellOffset = 3;

    public float perlinScale = 1;

    public float mountainHeight = 5;


    Vector2 pixelCellSize;
    Vector2 peakPosition;
    TerrainData terrainData;
    float[,] heights;
    void Start()
    {

        //terrainData = GetComponent<Terrain>().terrainData;
        //TerrainGeneration();
        
    }

    public void TerrainGeneration(GameObject[][] mapCellMatrix){


        terrainData = GetComponent<Terrain>().terrainData;
        float sizeAdjuster = terrainData.heightmapScale.x;
        pixelCellSize = cellSize / sizeAdjuster;

        
        //We make the terrain plain
        float[,] plain = terrainData.GetHeights(0,0,512,512);
        for (int i = 0; i < 512; i++)
        {
            for (int j = 0; j < 512; j++)
            {
                //Debug.Log(i.ToString() + " " + j.ToString());
                plain[i,j] = 0;
            }   
        }
        terrainData.SetHeights(0,0,plain);

 
        
        
        //We offset the terrain
        transform.position = Vector3.zero;
        Vector3 newPosition = transform.position;
        newPosition.x -= cellOffset * cellSize.x;
        newPosition.z -= cellOffset * cellSize.y;
        Vector2 zeroReferencePosition = new Vector2(cellOffset * cellSize.x,cellOffset * cellSize.y);
        transform.position = newPosition;


        //We put a mountain on every place that should be one
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                switch(mapCellMatrix[i][j].GetComponent<MapCell>().terrainType){
                    case MapCellType.Mountain:
                        placeMountain(zeroReferencePosition + new Vector2(i * cellSize.x, j * cellSize.y));
                        break;
                    default:
                        break;
                }
            }
        }




        
        
        //Debug.Log(terrainData.heightmapScale);

    }

    public void placeMountain(Vector2 zeroReferencePosition){
        //We will put a mountain in the 0,0 position

        peakPosition = (new Vector2(pixelCellSize.x/2.0f,pixelCellSize.y/2.0f)); 

        heights = new float[(int)Mathf.Ceil(pixelCellSize.x), (int)Mathf.Ceil(pixelCellSize.y)];
        for (int i = 0; i < pixelCellSize.x; i++)
        {
            for (int j = 0; j < pixelCellSize.y; j++)
            {
                //Debug.Log(heights.Length);
                //Debug.Log(i.ToString() + " " + j.ToString());
                heights[i,j] = MountainValue(i,j);
            }
        }
        zeroReferencePosition /= terrainData.heightmapScale.x;
        terrainData.SetHeights((int)Mathf.Ceil(zeroReferencePosition.x),(int)Mathf.Ceil(zeroReferencePosition.y),heights);

    }

    float MountainValue(int i, int j){
        //Debug.Log(i.ToString() + " " + j.ToString());
        float mountainValue  = Mathf.PerlinNoise(i / 50.0f * perlinScale,j / 50.0f  * perlinScale);

        Vector2 currentPosition = new Vector2((float)i,(float)j);

        float dividerValue = (currentPosition - peakPosition).magnitude;

        mountainValue -= (dividerValue/(pixelCellSize.x/2));
        mountainValue /= mountainHeight;
        
        return mountainValue;
    }


}
