using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penetration_Tile : MonoBehaviour {

    private BoxCollider2D m_BoxCollider;
    public GameObject playerPhysics;
    
    private float tileResetTime = 0;
    private bool isPlayerOn = false;

    public bool IsPlayerOn
    {
        get
        {
            return isPlayerOn;
        }

        set
        {
            isPlayerOn = value;
        }
    }

    // Use this for initialization
    void Start () {
        m_BoxCollider = GetComponent<BoxCollider2D>();
        playerPhysics = GameObject.FindWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        //if (playerPhysics.GetComponent<PhysicsObject>().Grounded == true)
        //    tileResetTime = 0.1f;
        //else
        //{
        //    if(tileResetTime < 0)
        //       m_BoxCollider.isTrigger = true;
        //    tileResetTime -= Time.deltaTime;
        //}
        //else
        //    tileResetTime -= Time.deltaTime;
        if(playerPhysics.GetComponent<PhysicsObject>().Grounded == false)
            m_BoxCollider.isTrigger = true;
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player_Bottom")
    //    {
    //        m_BoxCollider.isTrigger = false;
    //        playerPhysics.GetComponent<PhysicsObject>().Grounded = true;
    //        collision.transform.Translate(new Vector2(collision.transform.position.x, transform.position.y + 0.5f));
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player_Bottom")
        {
            float pVelocity = playerPhysics.GetComponent<PhysicsObject>().Velocity.y;
            tileResetTime = 10f;
            Vector3 objPos = collision.gameObject.transform.parent.transform.position;
            if (pVelocity <= 1)
            {
                m_BoxCollider.isTrigger = false;
                playerPhysics.GetComponent<PhysicsObject>().Grounded = true;
                collision.gameObject.transform.parent.transform.position = new Vector2(objPos.x, transform.position.y + 1.0f);
            }
            //m_BoxCollider.isTrigger = false;
            //playerPhysics.GetComponent<PhysicsObject>().Grounded = true;
            //Vector3 objPos = collision.gameObject.transform.parent.transform.position
            //collision.gameObject.transform.parent.transform.position = new Vector2(objPos.x, transform.position.y + 1.0f);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player_Bottom")
        {
            float pVelocity = playerPhysics.GetComponent<PhysicsObject>().Velocity.y;
            tileResetTime = 10f;
            Vector3 objPos = collision.gameObject.transform.parent.transform.position;
            if (pVelocity <= 1)
            {
                m_BoxCollider.isTrigger = false;
                playerPhysics.GetComponent<PhysicsObject>().Grounded = true;
                collision.gameObject.transform.parent.transform.position = new Vector2(objPos.x, transform.position.y + 1.0f);
            }
        }
    }
}
