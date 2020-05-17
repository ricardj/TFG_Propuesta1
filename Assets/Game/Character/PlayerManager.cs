using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{


    FPMovementController fpMovementController; 
    // Start is called before the first frame update
    void Start()
    {
        fpMovementController = GameObject.FindObjectOfType<FPMovementController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getPlayerDirectionAngle()
    {
        Vector3 cameraVector = Camera.main.transform.TransformDirection(Vector3.forward);
        cameraVector.y = 0;
        float cameraAngle = Vector3.Angle(Vector3.forward, cameraVector);
        cameraAngle = cameraAngle < 0 ? cameraAngle * -1 : cameraAngle;
        cameraAngle = cameraAngle < 0 ? 180 + 180 + cameraAngle : cameraAngle;

        cameraAngle = Camera.main.transform.eulerAngles.y;
        //Debug.Log(cameraAngle);
        return cameraAngle;
    }

    public void uiMapActivated()
    {
        fpMovementController.freezeControls = true;
    }

    public void uiMapDeactivated()
    {
        fpMovementController.freezeControls = false;
    }
}
