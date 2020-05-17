using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{

    public delegate void PauseAction();
    public static event PauseAction OnPause;

    public delegate void UnpauseAction();
    public static event UnpauseAction OnUnpause;


    public bool gamePaused = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //We check the menu button
        if (Input.GetKeyDown(KeyCode.M) && !gamePaused)
        {
            //Debug.Log("Game paused.");
            OnPause();
            gamePaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.M) && gamePaused)
        {
            //Debug.Log("Game unpaused");
            OnUnpause();
            gamePaused = false;
        }
    }


}
