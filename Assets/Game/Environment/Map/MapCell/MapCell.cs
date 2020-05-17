using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MapDesign;

public class MapCell : MonoBehaviour
{



    public MapCellType terrainType;
    public bool containsPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") containsPlayer = true;
        
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") containsPlayer = false;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player") containsPlayer = true;
    }

}
