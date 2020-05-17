using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMapGenerator : MonoBehaviour
{

    public GameObject uiMapCell;
    public GameObject[][] uiMapMatrix;
    public GameObject uiMapHolder;

    [HideInInspector]
    public MapManager mapManager;
    public UIMapManager uIMapManager;

    public float offsetX = 0;
    public float offsetY = 0;


    // Start is called before the first frame update
    void Start()
    {
        uiMapHolder = GameObject.FindGameObjectWithTag("UIMapHolder");
        mapManager = PGameManager.Instance.mapManager.GetComponent<MapManager>();
        uIMapManager = PGameManager.Instance.uiMapManager.GetComponent<UIMapManager>();

        if (mapManager == null)
        {
            Debug.LogError("Es necesario un MapManager para el correcto funcionamiento de este objeto.");
        }

        generateUiMap();
    }

    public bool update = false;
    // Update is called once per frame
    void Update()
    {
        if (update)
        {
            generateUiMap();
            update = false;
        }
    }

    public void generateUiMap()
    {
        GameObject[] uiMapCells = GameObject.FindGameObjectsWithTag("UIMapCell");
        foreach (GameObject cell in uiMapCells) DestroyImmediate(cell);

        int xSize = (int)uIMapManager.uiMapSize.x;
        int ySize = (int)uIMapManager.uiMapSize.y;

        uiMapMatrix = new GameObject[xSize][];
        for (int i = 0; i < xSize; i++)
        {
            uiMapMatrix[i] = new GameObject[ySize];
            for (int j = 0; j < ySize; j++)
            {

                uiMapMatrix[i][j] = Instantiate(uiMapCell, uiMapHolder.transform);

                Vector3 uiPosition = Vector3.zero;
                uiPosition.y -= -offsetX * xSize / 2 + offsetX / 2 + i * offsetX; //
                uiPosition.x -= -offsetY * ySize / 2 + offsetY / 2 + j * offsetY; //
                uiPosition.z = 0;
                //Debug.Log(uiPosition);
                //uiMapMatrix[i][j].transform.position = uiMapCell.transform.(uiPosition);
                uiMapMatrix[i][j].transform.localPosition = transform.TransformPoint(uiPosition);
                //uiMapMatrix[i][j].transform.position = uiPosition;

                //Debug.Log(mapManager.mapMatrix);

                //uiMapMatrix[i][j].GetComponent<UIMapCell>().mapCellType = mapManager.mapMatrix[i][j].GetComponent<MapCell>().terrainType;
                //For now we will have a default cell Type
                
                //uiMapMatrix[i][j].GetComponent<UIMapCell>().updateUiCell(new MapCellInfo( MapDesign.MapCellType.Plain));
            }
        }
        PGameManager.Instance.uiMapManager.GetComponent<UIMapManager>().uiMapMatrix = uiMapMatrix;
    }
}
