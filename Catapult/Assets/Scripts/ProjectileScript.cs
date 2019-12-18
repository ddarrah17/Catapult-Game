using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectileScript : MonoBehaviour {

    public float maxStretch = 1.5f;
    public LineRenderer catLineFront;
    public LineRenderer catLineBack;
    public GameObject nextRock; 
    
    private SpringJoint2D spring;
    private Transform catapult; 
    private Ray rayToMouse;
    private Ray leftCatapultToProjectile;
    private float maxStretchSqr;
    private float circleRadius;
    private bool clickedOn = false;
    private Vector2 prevVelocity;
    private Rigidbody2D rgbd; 

    void Awake(){
        spring = GetComponent<SpringJoint2D>();
        rgbd = GetComponent<Rigidbody2D>();
        catapult = spring.connectedBody.transform;
    }

    // Use this for initialization
    void Start () {
        LineRendererSetup();
        rayToMouse = new Ray(catapult.position, Vector3.zero);
        leftCatapultToProjectile = new Ray(catLineFront.transform.position, Vector3.zero);
        maxStretchSqr = maxStretch * maxStretch;
        CircleCollider2D circle = GetComponent<CircleCollider2D>();
        circleRadius = circle.radius;
    }

    // Update is called once per frame
    void Update() {
        if(clickedOn){
            Dragging(); 
        } 
        if (spring != null){
            if(!rgbd.isKinematic && prevVelocity.sqrMagnitude > rgbd.velocity.sqrMagnitude){
                Destroy(spring);
                rgbd.velocity = prevVelocity;
            }
            if (!clickedOn){
                prevVelocity = rgbd.velocity; 
            }
            LineRendererUpdate();
        } else {
            catLineBack.enabled = false;
            catLineFront.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
    }

    void LineRendererSetup(){
        catLineFront.SetPosition(0, catLineFront.transform.position);
        catLineBack.SetPosition(0, catLineBack.transform.position);

        catLineFront.sortingLayerName = "ForeGround";
        catLineBack.sortingLayerName = "ForeGround";

        catLineFront.sortingOrder = 3;
        catLineBack.sortingOrder = 1; 
    }

    void OnMouseDown(){
        spring.enabled = false;
        clickedOn = true;

    }

    void OnMouseUp(){
        spring.enabled = true;
        clickedOn = false;
        rgbd.isKinematic = false;
        StartCoroutine(Wait()); 
    }

    void Dragging(){
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 catapultToMouse = mouseWorldPoint - catapult.position; 

        if(catapultToMouse.sqrMagnitude > maxStretchSqr){
            rayToMouse.direction = catapultToMouse;
            mouseWorldPoint = rayToMouse.GetPoint(maxStretch);
        }

        mouseWorldPoint.z = 0f;
        transform.position = mouseWorldPoint; 
    }

    void LineRendererUpdate(){
        Vector2 catapultToProjectile = transform.position - catLineFront.transform.position;
        leftCatapultToProjectile.direction = catapultToProjectile;
        Vector3 holdPoint = leftCatapultToProjectile.GetPoint(catapultToProjectile.magnitude + circleRadius);
        catLineBack.SetPosition(1, holdPoint);
        catLineFront.SetPosition(1, holdPoint);
    }

    void ResetGame()
    {
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        if (nextRock != null)
        {
            nextRock.SetActive(true);
        }
        else
        {

        }
    }


}
