using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMapManager : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]
    public GameObject[][] uiMapMatrix; //Matrix of uiMapCells
    public Vector2 uiMapSize = new Vector2(5,5);

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void refreshUIMap(MapCellInfo[][] mapCellInfo) //Matrix of mapCells
    {
        //We go all over the ui matrix setting the values in the uiMapCEll
        int x = (int)uiMapSize.x;
        int y = (int)uiMapSize.y;

        for (int i = 0; i < x; i++)
        {
            for(int j = 0; j < y; j++)
            {
                //if (uiMapMatrix[i][j].GetComponent<UIMapCell>().focusAudios.Length == 0) Debug.Log("Cell : " + i.ToString() + " " + j.ToString());
                //Debug.Log("Calling to update all the cells.");
                uiMapMatrix[i][j].GetComponent<UIMapCell>().updateUiCell(mapCellInfo[i][j]);
            }
        }
    }
}
