using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletEmitter;
    public float bulletForwardForce = 30f;
    public float bulletDeathTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject temporalBulletHandler;
            temporalBulletHandler = Instantiate(bullet, bulletEmitter.transform.position, bulletEmitter.transform.rotation) as GameObject;

            temporalBulletHandler.GetComponent<Rigidbody>().AddForce(bulletEmitter.transform.forward*bulletForwardForce);

            Destroy(temporalBulletHandler, bulletDeathTime);
            GetComponent<AudioSource>().Play();
        }
    }
}
