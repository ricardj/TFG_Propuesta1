using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MapDesign;

public class AmbientEmitter : MonoBehaviour
{

    [HideInInspector]
    public AudioSource environmentSound;

    // Start is called before the first frame update
    void Start()
    {
        MapCellType terrainType = transform.parent.gameObject.GetComponent<MapCell>().terrainType;
        environmentSound = GetComponent<AudioSource>();
        environmentSound.clip = MapDesign.MapCellTypeSounds[terrainType];
        environmentSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
