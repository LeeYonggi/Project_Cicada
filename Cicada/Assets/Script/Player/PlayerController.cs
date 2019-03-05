﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : PhysicsObject{
    const float mobileSpeed = 1.0f;

    enum DIRECTION
    {
        NONE,
        LEFT,
        RIGHT
    }
    enum PLAYER_WEAPON_STATE
    {
        SPEAR,
        PICKEL,
        SNORKEL
    }
    private DIRECTION moveDirection;
    private PLAYER_WEAPON_STATE player_weapon_state;

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    public float attackDelay;

    private bool isAttack;
    private bool isAttacked;
    private bool isClimbing;
    private bool isTouched;
    public  bool isGrounded;
    private bool isPushing;
    [HideInInspector] public  bool isInvincible;

    private SpriteRenderer spriteRenderer;
    private Animator m_Animator;
    private Rigidbody2D rb;

    Vector2 directionX;
    Vector2 move;

    public GameObject attackPrefeb;

    private float attackedMoveX;
    private bool climbingFlip;
    private bool isClimbJump;
    private bool pushingFlip;

    private Vector2 pastMove;
    private Vector2 tileMoveVector;
    public Camera mainCamera;
    public GameObject landingEffect;

    private AudioSource[] audioSources;

    #region GETSET
    public Vector2 TileMoveVector
    {
        get
        {
            return tileMoveVector;
        }

        set
        {
            tileMoveVector = value;
        }
    }
    #endregion
    //EventTrigger uievent;

    // Use this for initialization
    void Start () {
        Init();
        spriteRenderer = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isAttack = false;
        isAttacked = false;
        isClimbing = false;
        isClimbJump = false;
        isGrounded = false;
        isInvincible = false;
        isPushing = false;

        tileMoveVector = Vector2.zero;
        pastMove = Vector2.zero;
        player_weapon_state = PLAYER_WEAPON_STATE.SPEAR;

        audioSources = GetComponents<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        PysicsUpdate();
        Vector2 horizontal = Vector2.zero;
        horizontal.x = Input.GetAxis("Horizontal");
        if (horizontal.x != 0.0f)
        {
            if (!audioSources[0].isPlaying) audioSources[0].Play();

            move = horizontal;
            MovePlayer();
            move = Vector2.zero;
        }
        else
        {
            audioSources[0].Stop();
            MovePlayer();
        }

        if (Input.GetButtonDown("Jump"))
        {
            JumpStart();
        }
        else if(Input.GetButtonUp("Jump"))
        {
            JumpEnd();
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            PlayerAttack();
        }

        if(isPushing)
        {
            if (pushingFlip != spriteRenderer.flipX ||
                isJump)
                isPushing = false;
                
        }

        if(grounded)
        {
            isClimbing = false;
            if (isClimbJump)
            {
                isClimbJump = false;
                move = pastMove;
            }
        }

        ClimbingJump();

        m_Animator.SetInteger("Weapon_State", (int)player_weapon_state);
        m_Animator.SetBool("isJump", isJump);
        m_Animator.SetBool("grounded", grounded);
        m_Animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
        m_Animator.SetBool("isAttack", isAttack);
        m_Animator.SetBool("isClimbing", isClimbing);
        m_Animator.SetBool("isPushing", isPushing);
    }

    #region Player_Attack
    public void PlayerAttack()
    {
        if (grounded && isAttack == false)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    private void CreatePlayerAttack()
    {
        GameObject obj = Instantiate(attackPrefeb, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
        obj.GetComponent<PlayerAttack>().AttackInit(attackDelay * 0.7f, gameObject);
    }

    IEnumerator AttackCoroutine()
    {
        isAttack = true;
        yield return new WaitForSeconds(attackDelay * 0.3f);

        CreatePlayerAttack();

        yield return new WaitForSeconds(attackDelay * 0.7f);
        isAttack = false;
    }
    #endregion

    #region Player_Jump
    public void JumpStart()
    {
        if (isJump == true || isAttack) return;
        if (isAttacked && velocity.y > 0) return;
        if (isClimbing)
        {
            isClimbJump = true;
            if (Input.touchCount <= 1)
                isTouched = false;
            else
                isTouched = true;
        }
        else
        {
            audioSources[1].Play();
            JumpPlayer();
        }
    }

    public void JumpEnd()
    {
        if (velocity.y > 0)
        {
            velocity.y = velocity.y * 0.6f;
        }
    }

    public void JumpPlayer()
    {
        velocity.y += jumpTakeOffSpeed;
        isJump = true;
    }
    #endregion

    #region Player_Move
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
        targetVelocity = move * maxSpeed + tileMoveVector;
        if (isAttack)
            targetVelocity = Vector2.zero;
        tileMoveVector = Vector2.zero;
    }

    public void MoveLeft()
    {
        pastMove = Vector2.left * mobileSpeed;
        if (!isClimbJump)
            move = Vector2.left * mobileSpeed;
    }
    public void MoveRight()
    {
        pastMove = Vector2.right * mobileSpeed;
        if (!isClimbJump)
            move = Vector2.right * mobileSpeed;
    }
    public void StopMove()
    {
        pastMove = Vector2.zero;
        if (!isClimbJump)
            move = Vector2.zero;
    }
    #endregion

    void ClimbingJump()
    {
        if(isClimbJump)
        {
            if (climbingFlip == false)
            {
                move = Vector2.left * mobileSpeed;
                
                if (!isJump)
                {
                    transform.Translate(Vector2.left * 0.1f);
                    JumpPlayer();
                    if(isTouched)
                        pastMove = -move;
                }
            }
            if (climbingFlip == true)
            {
                move = Vector2.right * mobileSpeed;
                if (!isJump)
                {
                    transform.Translate(Vector2.right * 0.1f);
                    JumpPlayer();
                    if(isTouched)
                        pastMove = -move;
                }
            }
            //isClimbJump = false;
        }
    }

    public void CallAttackedCoroutine(int time)
    {
        StartCoroutine(AttackedCoroutine(time));
    }

    IEnumerator AttackedCoroutine(int time)
    {
        isAttacked = true;

        audioSources[3].Play();
        
        for(int i = 0; i < time; i++)
        {
            if(i % 2 == 0)
                spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            else
                spriteRenderer.color = new Color(1, 1, 1, 1.0f);

            yield return new WaitForSeconds(1.0f * 0.3f);
        }
        spriteRenderer.color = new Color(1, 1, 1, 1.0f);
        isAttacked = false;
    }

    public void PlayerAttacked(int damage, Vector2 targetPos)
    {

        if (isAttacked || isInvincible) return;
        GetComponent<PlayerInfo>().AddAttacked(damage);
        StartCoroutine(AttackedCoroutine(3));
        if (targetPos.y <= transform.position.y + 0.1f && velocity.y <= 0.0f)
            velocity.y = jumpTakeOffSpeed * 0.4f;

        mainCamera.GetComponent<CameraController>().ShakeCamera();
        //attackedMoveX = (transform.position.x - targetPos.x) * 0.25f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;

            audioSources[2].Play();

            GameObject obj = Instantiate(landingEffect, new Vector3(transform.position.x, transform.position.y - 0.45f), Quaternion.identity);
            Destroy(obj, 0.3f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "MonsterCol")
        {
            PlayerAttacked(1, collision.transform.position);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            if(grounded == false && move.x != 0)
            {
                climbingFlip = spriteRenderer.flipX;
                isClimbing = true;
                velocity.y = 0;
                isJump = false;
                
            }
        }
        if(collision.gameObject.tag == "Rock")
        {
            Vector3 distance = collision.gameObject.transform.position;
            distance = distance - transform.position;
            distance.y = 0;
            Vector3.Normalize(distance);
            collision.GetComponent<Rigidbody2D>().velocity = distance * 4;
            isPushing = true;
            pushingFlip = spriteRenderer.flipX;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isClimbing = false;
        }
    }

    public void BeInvincibleForSec(float time)
    {
        StartCoroutine(_BeInvincibleForSec(time));
    }

    private IEnumerator _BeInvincibleForSec(float time)
    {
        isInvincible = true;

        yield return new WaitForSeconds(time);

        isInvincible = false;
    }
}
