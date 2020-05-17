using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionPlayingMenu : MonoBehaviour
{
    AudioSource[] instructions;
    int instructionIndex = 0;
    public float startTime = 3f;
    bool instructionsPlaying;
    
    void Start()
    {

        instructions = GetComponents<AudioSource>();
        StartCoroutine(PlayInstruction(startTime));
        instructionsPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && instructionsPlaying)
        {
            StopAllCoroutines();
            foreach (AudioSource audioSource in instructions) audioSource.Stop();
            instructionsPlaying = false;
        }
    }

    IEnumerator PlayInstruction(float delay)
    {
        yield return new WaitForSeconds(delay);
        instructions[instructionIndex].Play();
        Debug.Log("Played instruction " + instructionIndex.ToString());
        yield return new WaitForSeconds(instructions[instructionIndex].clip.length);
        instructionIndex++;
        if (instructionIndex < instructions.Length)
        {
            
            StartCoroutine(PlayInstruction(0));
        }
    }
}
