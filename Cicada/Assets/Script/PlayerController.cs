﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : PhysicsObject{

    enum DIRECTION
    {
        NONE,
        LEFT,
        RIGHT
    }
    private DIRECTION moveDirection;

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    private SpriteRenderer spriteRenderer;
    private Animator m_Animator;
    private Rigidbody2D rb;

    Vector2 directionX;
    Vector2 move;


    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
        Vector2 horizontal = Vector2.zero;
        horizontal.x = Input.GetAxis("Horizontal");
        if (horizontal.x != 0.0f)
        {
            move = horizontal;
            MovePlayer();
            move = Vector2.zero;
        }
        else
            MovePlayer();

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
            isJump = true;
        }
        else if(Input.GetButtonUp("Jump"))
        {
            if(velocity.y > 0){
                velocity.y = velocity.y * 0.5f;
            }
        }

        m_Animator.SetBool("isJump", isJump);
        m_Animator.SetBool("grounded", grounded);
        m_Animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
        
	}

    void JumpPlayer()
    {
        rb.AddForce(Vector2.up * jumpTakeOffSpeed, ForceMode2D.Impulse);
        isJump = true;
    }

    void MovePlayer()
    {
        if (move.x > 0.01f)
        {
            if (spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
            }
        }
        else if (move.x < -0.01f)
        {
            if (spriteRenderer.flipX == false)
            {
                spriteRenderer.flipX = true;
            }
        }
        targetVelocity = move * maxSpeed;
    }

    public void MoveLeft()
    {
        move = Vector2.left * 0.7f;
    }
    public void MoveRight()
    {
        move = Vector2.right * 0.7f;
    }
    public void StopMove()
    {
        move = Vector2.zero;
    }
}
