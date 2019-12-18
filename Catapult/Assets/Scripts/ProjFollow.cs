using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjFollow : MonoBehaviour {

    public Transform projectile;
    public Transform projectile2;
    public Transform projectile3; 
    public Transform farLeft;
    public Transform farRight;

    bool cam1;
    bool cam2; 

    void Start()
    {
        GameObject mainCam = GameObject.Find("MainCamera");
        ResetScript cam = mainCam.GetComponent<ResetScript>();
        cam1 = cam.resetCamera1;
        cam2 = cam.resetCamera2; 
    }

// Update is called once per frame
void Update () {
        // GameObject mainCam = GameObject.Find("MainCam");
        // ResetScript cam = mainCam.GetComponent<ResetScript>();

        if (cam1 == false)
        {
            Vector3 newPos = transform.position;
            newPos.x = projectile.position.x + 5;
            newPos.x = Mathf.Clamp(newPos.x, farLeft.position.x, farRight.position.x);
            transform.position = newPos;
        }
        else if(cam1 == true){
            Debug.Log("dss");
            Vector3 newPos = transform.position;
            newPos.x = projectile2.position.x + 5;
            newPos.x = Mathf.Clamp(newPos.x, farLeft.position.x, farRight.position.x);
            transform.position = newPos;
        }
    }
}
