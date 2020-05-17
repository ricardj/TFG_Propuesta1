using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NorthPointBehaviour : MonoBehaviour
{
    public float distance = 30f;
    GameObject player;
    public float heightRange = 6;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 north;
        north.x =  player.transform.position.x + distance;
		north.y = player.transform.position.y;
		north.z = player.transform.position.z;
		transform.position = north;

        GetComponent<AudioSource>().pitch = transform.position.y/heightRange+ 1;
	}
}
