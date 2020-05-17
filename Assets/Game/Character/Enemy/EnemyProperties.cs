using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties : MonoBehaviour
{
    public const float START_LIFE = 100f;
    public float life = 100f;

    public AudioClip hittedEnemy;
    public AudioClip enemySound;

    public GameObject killedEnemy;


    AudioSource[] audioSources;
    // Start is called before the first frame update
    void Start()
    {
        life = START_LIFE;
        audioSources = gameObject.GetComponents<AudioSource>();

        if(audioSources.Length < 2)
            Debug.LogWarning("Not enough audio sources for the game object.");

        //We init the constant audio source
        audioSources[0].clip = enemySound;
        audioSources[0].loop = true;
        audioSources[0].Play();


    }

    // Update is called once per frame
    void Update()
    {
        if(life <= 0 )
        {
            Instantiate(killedEnemy,transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }

    public void SubstractLife(float damage)
    {
        audioSources[1].PlayOneShot(hittedEnemy);
        //AudioSource alien = GetComponents<AudioSource>()[1];
        //alien.Play();
        life -= damage;
    }

 
}
