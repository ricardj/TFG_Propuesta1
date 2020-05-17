using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayOption : MonoBehaviour
{
    // Start is called before the first frame update
    CubeActions cubeActions;
    void Start()
    {
        cubeActions = GetComponent<CubeActions>();
        cubeActions.OnSelect += LoadNextLevel;
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
