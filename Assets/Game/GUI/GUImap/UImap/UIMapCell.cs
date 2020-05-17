using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static MapDesign;
using static UIMapExplorer;

public class UIMapCell : MonoBehaviour
{
    [HideInInspector]
    public bool hasFocus;
    AudioSource[] focusAudios;
    Color originalImageColor;

    [HideInInspector]
    public MapCellInfo mapCellInfo;

    private AudioSource mapCellAudio;

    [HideInInspector]
    public bool uiCellReady = false;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        focusAudios = new AudioSource[2];
        focusAudios = GetComponents<AudioSource>();


        updateUiCell(new MapCellInfo(MapCellType.Plain));


        originalImageColor = GetComponent<Image>().color;
        uiCellReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateUiCell(MapCellInfo mapCellInfo)
    {
        this.mapCellInfo = mapCellInfo;
        focusAudios[1].clip = NavigationSounds[this.mapCellInfo.mapCellType];

        //We set the cell one color or another
        Color newColor = new Color();
        switch(this.mapCellInfo.mapCellType){
            case MapCellType.Plain:
                newColor = Color.green;
                break;
            case MapCellType.Mountain:
                newColor = Color.yellow;
                break;
            default:
                newColor = Color.gray;
                break;
        }

        GetComponent<Image>().color = newColor;
        originalImageColor = GetComponent<Image>().color;
    }

    public void setFocus(bool focus)
    {
        hasFocus = focus;
        if (hasFocus)
        {
            foreach (AudioSource audioSource in focusAudios)
            {
                audioSource.Play();
            }
            GetComponent<Image>().color = Color.black;
        }
        else if (!hasFocus)
        {
            //Debug.Log(focusAudios);
            foreach (AudioSource audioSource in focusAudios)
            {
                audioSource.Stop();
            }
            GetComponent<Image>().color = originalImageColor;
        }
    }

    public void playInfo()
    {
        focusAudios[1].Play();
    }
}
