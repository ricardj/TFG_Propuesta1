using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float damage = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int bulletNumber = 0;
    public void OnCollisionEnter(Collision collision)
    {

        switch (collision.gameObject.tag)
        {
            case "Player":
                //Debug.Log("Bullet collided player: " + bulletNumber.ToString());
                break;

            case "Enemy":
                collision.gameObject.GetComponent<EnemyProperties>().SubstractLife(damage);
                Destroy(gameObject);
                break;

            default:
                //Debug.Log("Bullet destroyed itself because of "+ collision.gameObject.tag +": " + bulletNumber.ToString());
                Destroy(gameObject);
                break;

        }
        bulletNumber++;
        
       
    }
}
