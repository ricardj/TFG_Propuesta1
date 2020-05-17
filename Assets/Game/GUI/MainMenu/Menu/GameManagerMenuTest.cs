using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerMenuTest : MonoBehaviour
{

    //We get all the options
    GameObject[] options;
    // Start is called before the first frame update
    void Start()
    {
        options = GameObject.FindGameObjectsWithTag("Option");
    }

    // Update is called once per frame
    bool firstSet = false;
    int selectedOption = 0;
    
    void Update()
    {
        
        
        if (firstSet)
        {

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if ((selectedOption + 1) % 3 != 0)
                {
                    options[selectedOption].GetComponent<CubeActions>().SetFocus(false);
                    selectedOption++;
                    options[selectedOption].GetComponent<CubeActions>().SetFocus(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if(selectedOption % 3 != 0)
                {
                    options[selectedOption].GetComponent<CubeActions>().SetFocus(false);
                    selectedOption--;
                    options[selectedOption].GetComponent<CubeActions>().SetFocus(true);
                }
            }

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (selectedOption < 6)
                {
                    options[selectedOption].GetComponent<CubeActions>().SetFocus(false);
                    selectedOption+=3;
                    options[selectedOption].GetComponent<CubeActions>().SetFocus(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (selectedOption > 2)
                {
                    options[selectedOption].GetComponent<CubeActions>().SetFocus(false);
                    selectedOption-=3;
                    options[selectedOption].GetComponent<CubeActions>().SetFocus(true);
                }
            }
        }

        if (Input.anyKey && !firstSet)
        {
            Debug.Log("Initiated Menu");
            if (options != null && options.Length > 0)
            {
                selectedOption = 4;
                options[selectedOption].GetComponent<CubeActions>().SetFocus(true);
            }
            firstSet = true;
        }
    }
}
