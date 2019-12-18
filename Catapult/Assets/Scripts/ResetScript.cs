using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ResetScript : MonoBehaviour {
    public Rigidbody2D proj;
    public Rigidbody2D proj2;
    public Rigidbody2D proj3; 
    public float resetSpeed = 0.025f;
    public bool resetCamera1 = false;
    public bool resetCamera2 = false;
    public bool resetCamera3 = false;

    public Transform farLeft;
    public Transform farRight;
    int lifeCount = 2; 

    private float resetSpeedSqr;
    private SpringJoint2D spring;
    private SpringJoint2D spring2;
    private SpringJoint2D spring3;
    // Use this for initialization
    void Start () {
        resetSpeedSqr = resetSpeed * resetSpeed;
        spring = proj.GetComponent<SpringJoint2D>();
        spring2 = proj2.GetComponent<SpringJoint2D>();
        spring3 = proj3.GetComponent<SpringJoint2D>();
    }

    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene("TitleScene");
        }
        if (spring == null && proj.velocity.sqrMagnitude < resetSpeedSqr)
        {
            //ResetGame();
            resetCamera1 = true;
        }
        if (resetCamera1 == true && spring2 == null && proj2.velocity.sqrMagnitude < resetSpeedSqr)
        {
            //ResetGame();
            resetCamera2 = true;
        }
        if (resetCamera2 == true && spring3 == null && proj3.velocity.sqrMagnitude < resetSpeedSqr)
        {
            //ResetGame();
            resetCamera3 = true;
        }
        if (resetCamera1 == false)
        {
            Vector3 newPos = transform.position;
            newPos.x = proj.position.x + 5;
            newPos.x = Mathf.Clamp(newPos.x, farLeft.position.x, farRight.position.x);
            transform.position = newPos;
        }
        if (resetCamera1 == true)
        {
            Vector3 newPos = transform.position;
            newPos.x = proj2.position.x + 5;
            newPos.x = Mathf.Clamp(newPos.x, farLeft.position.x, farRight.position.x);
            transform.position = newPos;
            lifeCount--;
        }
        if(resetCamera2 == true)
        {
            Vector3 newPos = transform.position;
            newPos.x = proj3.position.x + 5;
            newPos.x = Mathf.Clamp(newPos.x, farLeft.position.x, farRight.position.x);
            transform.position = newPos;
            lifeCount--;
        }
        if(resetCamera3 == true){
            //ResetGame();
            
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent <Rigidbody2D>() == proj) {
           //ResetGame();
        }
    }

   
    void ResetGame(){
        SceneManager.LoadScene("TitleScene");
    }
}
