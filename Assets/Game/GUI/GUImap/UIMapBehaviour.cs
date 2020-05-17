using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMapBehaviour : MonoBehaviour
{


    public bool mapDisplayed = false;
    GameObject mapHolder;
    UIMapExplorer uiMapExplorer;

    PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        mapHolder = GameObject.FindGameObjectWithTag("UIMapHolder");
        uiMapExplorer = mapHolder.GetComponent<UIMapExplorer>();

        //uiMapExplorer.deactivateUIMap();
        //We should deactivate all the cells when theyre all started

        StartCoroutine(WaitAllCellsStarted());


        playerManager = PGameManager.Instance.GetComponent<PGameManager>().playerManager.GetComponent<PlayerManager>();

        if(playerManager == null)
        {
            Debug.LogWarning("Error: UIMap behaviour needs to contact the player manager.");
        }
        
    }

    IEnumerator WaitAllCellsStarted()
    {
        bool uiCellsReady = false;
        while(!uiCellsReady )
        {
            uiCellsReady = true;
            GameObject[] uiMapCells = GameObject.FindGameObjectsWithTag("UIMapCell");
            foreach(GameObject uiMapCell in uiMapCells)
            {
                uiCellsReady = uiCellsReady && uiMapCell.GetComponent<UIMapCell>().uiCellReady;
                
            }
            yield return new WaitForSeconds(.1f); ;
        }
        mapHolder.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {   

        if (Input.GetKeyDown(KeyCode.E) && !mapDisplayed)
        {
            mapDisplayed = true;

            playerManager.uiMapActivated();

            activateUIMap();


        }
        else if (Input.GetKeyDown(KeyCode.E) && mapDisplayed)
        {
            mapDisplayed = false;

            playerManager.uiMapDeactivated();

            deactivateUIMap();
        }
    }

    void deactivateUIMap()
    {
        mapHolder.SetActive(false);
        uiMapExplorer.deactivateUIMap();
        
    }

    void activateUIMap()
    {
        mapHolder.SetActive(true);
        uiMapExplorer.activateUIMap();
    }
}
