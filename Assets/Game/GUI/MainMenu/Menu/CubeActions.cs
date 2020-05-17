using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeActions : MonoBehaviour
{
    Color originalColor;
    Color shinnyColor;
    [HideInInspector]
    public bool hasFocus;

    public delegate void SelectAction();
    public event SelectAction OnSelect;

    public GameObject selectionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        originalColor = GetComponent<Renderer>().material.color;
        shinnyColor = new Color(1.0f, 0f, 0f);
    }

    GameObject selectionPrefabHandler;
    // Update is called once per frame
    void Update()
    {
        if(hasFocus && Input.GetKeyDown(KeyCode.Return))// && selectionPrefabHandler == null)
        {
            selectionPrefabHandler = Instantiate(selectionPrefab, transform.position, transform.rotation);
            if(OnSelect != null)
            {
                OnSelect();
            }
        }
    }

    public void SetFocus(bool hasFocus)
    {
        this.hasFocus = hasFocus;
        if (hasFocus)
        {
            AudioSource[] audios = GetComponents<AudioSource>();
            foreach (AudioSource clip in audios) clip.Play();
            GetComponent<Renderer>().material.color = shinnyColor;
        }
        else
        {
            AudioSource[] audios = GetComponents<AudioSource>();
            foreach (AudioSource clip in audios) clip.Stop();
            GetComponent<Renderer>().material.color = originalColor;
        }
    }


}
