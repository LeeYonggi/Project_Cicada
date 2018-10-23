using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : PhysicsObject{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    private SpriteRenderer spriteRenderer;
    private Animator m_Animator;

    Vector2 directionX;
    Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");
        directionX = CrossPlatformInputManager.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded){
            velocity.y = jumpTakeOffSpeed;
            isJump = true;
        }
        else if(Input.GetButtonUp("Jump"))
        {
            if(velocity.y > 0){
                velocity.y = velocity.y * 0.5f;
            }
        }

        if(move.x > 0.01f)
        {
            if(spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
            }
        }
        else if(move.x < -0.01f)
        {
            if(spriteRenderer.flipX ==false)
            {
                spriteRenderer.flipX = true;
            }
        }
        m_Animator.SetBool("isJump", isJump);
        m_Animator.SetBool("grounded", grounded);
        m_Animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
        targetVelocity = move * directionX * maxSpeed;
	}
}
