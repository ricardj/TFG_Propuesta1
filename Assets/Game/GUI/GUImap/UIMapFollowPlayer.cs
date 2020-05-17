using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UIMapFollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public float height = 0;
    GameObject player;




    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 uiMapPosition;
        uiMapPosition.x = player.transform.position.x;
        uiMapPosition.y = player.transform.position.y + height;
        uiMapPosition.z = player.transform.position.z;
        transform.position = uiMapPosition;

    }
}
