using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteamAudio;

[ExecuteInEditMode]
public class SteamAudioReseter : MonoBehaviour
{
    public bool update = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (update)
        {
            update = false;
            resetAllSteamAudioGameObjects();
        }
    }

    public void resetAllSteamAudioGameObjects()
    {
        SteamAudioSource[] steamAudioGameObjects = (SteamAudioSource[])GameObject.FindObjectsOfType(typeof(SteamAudioSource));
        foreach (SteamAudioSource source in steamAudioGameObjects)
        {
            GameObject steamAudioSourceGameObject = source.gameObject;
            DestroyImmediate(source);
            steamAudioSourceGameObject.AddComponent<SteamAudioSource>();
            Debug.Log(steamAudioSourceGameObject.name + "processed");
        }
    }
}
