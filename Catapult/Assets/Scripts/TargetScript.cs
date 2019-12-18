using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour {
    public int hitPoints = 2;
    public Sprite damagedSprite;
    public float damageImpactSpeed;
    public GameObject Blood; 

    private int currentHP;
    private float damageImpactSpeedSqr;
    private SpriteRenderer spriteRender;
    private Rigidbody2D rb2d;
    private Collider2D col; 

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    // Use this for initialization
    void Start () {
        spriteRender = GetComponent<SpriteRenderer>();
        currentHP = hitPoints;
        damageImpactSpeedSqr = damageImpactSpeed * damageImpactSpeed; 
    }

    void OnCollisionEnter2D(Collision2D collision){

        if (collision.collider.tag != "Damager"){
            return; 
        }
        if(collision.relativeVelocity.magnitude < damageImpactSpeedSqr){
            return; 
        }

        spriteRender.sprite = damagedSprite;
        currentHP--; 

        if(currentHP <= 0){
            Kill(collision);
            spriteRender.enabled = false;
        }
    }

    void Kill(Collision2D collision){
        Instantiate(Blood, transform.position, Quaternion.identity);
        col.enabled = false; 
        rb2d.isKinematic = true; 
    }

    // Update is called once per frame
    void Update () {
		
	}
}
