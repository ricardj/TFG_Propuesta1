using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHolder : MonoBehaviour
{
    GameObject[] options;
    int currentOption = 0;
    AudioSource backgroundMenuMusic;

    bool gamePaused = false;

    //public static event GamePaused;

    // Start is called before the first frame update
    void Start()
    {
        options = GameObject.FindGameObjectsWithTag("Option");
        foreach (GameObject option in options)
        {
            option.SetActive(false);
        }
        backgroundMenuMusic = GetComponent<AudioSource>();
        PauseManager.OnPause += pauseGame;
        PauseManager.OnUnpause += unpauseGame;
    }

    // Update is called once per frame
    void Update()
    {

        if (gamePaused)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                options[currentOption].GetComponent<CubeActions>().SetFocus(false);
                currentOption++;
                if (currentOption >= options.Length)
                {
                    currentOption = 0;
                }
                options[currentOption].GetComponent<CubeActions>().SetFocus(true);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                options[currentOption].GetComponent<CubeActions>().SetFocus(false);
                currentOption--;
                if (currentOption < 0)
                {
                    currentOption = options.Length - 1;
                }
                options[currentOption].GetComponent<CubeActions>().SetFocus(true);
            }
        }
    }



    public void pauseGame()
    {
        Time.timeScale = 0;
        gamePaused = true;
        backgroundMenuMusic.Play();
        foreach (GameObject option in options)
        {
            option.SetActive(true);
        }

        currentOption = 0;
        options[currentOption].GetComponent<CubeActions>().SetFocus(true);
    }

    public void unpauseGame()
    {
        Time.timeScale = 1;
        gamePaused = false;
        backgroundMenuMusic.Stop();
        foreach (GameObject option in options)
        {
            option.SetActive(false);
        }
    }
}
