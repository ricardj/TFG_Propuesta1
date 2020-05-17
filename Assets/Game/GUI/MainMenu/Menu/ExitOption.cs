using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitOption : MonoBehaviour
{
    CubeActions cubeActions;
    void Start()
    {
        cubeActions = GetComponent<CubeActions>();
        cubeActions.OnSelect += ExitGame;
    }

    void ExitGame()
    {
        Debug.Log("Quitting application.");
        Application.Quit();
    }
   
}
