using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationAssistance : MonoBehaviour
{

    // Start is called before the first frame update
    GameObject northPoint;
    
    void Start()
    {
        northPoint = GameObject.Find("NorthPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        { 
            northPoint.GetComponent<AudioSource>().Play();
        }
    }
}
