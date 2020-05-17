using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionOption : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying) Destroy(this.gameObject);
    }
}
