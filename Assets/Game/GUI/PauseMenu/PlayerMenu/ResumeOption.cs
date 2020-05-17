using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeOption : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<CubeActions>().hasFocus && Input.GetKeyDown(KeyCode.Return))
        {
            //transform.parent.gameObject.GetComponent<Menu>().unpauseGame();
        }
    }
}
