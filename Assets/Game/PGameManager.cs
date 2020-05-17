using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PGameManager : MonoBehaviour
{
    public static PGameManager Instance { get; private set; }

    public GameObject mapManager;
    public GameObject enemyManager;
    public GameObject pauseManager;
    public GameObject uiMapManager;
    public GameObject playerManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
