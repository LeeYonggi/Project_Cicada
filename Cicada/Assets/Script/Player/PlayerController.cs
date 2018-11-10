using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

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
    public float attackDelay;

    private bool isAttack;
    private bool isAttacked;
    private bool isClimbing;

    private SpriteRenderer spriteRenderer;
    private Animator m_Animator;
    private Rigidbody2D rb;

    Vector2 directionX;
    Vector2 move;

    public GameObject attackPrefeb;

    private float attackedMoveX;
    private bool climbingFlip;
    private bool isClimbJump;

    private Vector2 pastMove;
    public Camera mainCamera;
    public GameObject landingEffect;

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
        pastMove = Vector2.zero;
    }
	
	// Update is called once per frame
	void Update () {
        PysicsUpdate();
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

        //if(isAttacked)
        //{
        //    attackedMoveX -= attackedMoveX * 0.2f;
        //    transform.Translate(new Vector2(attackedMoveX, 0));
        //}

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

        m_Animator.SetBool("isJump", isJump);
        m_Animator.SetBool("grounded", grounded);
        m_Animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
        m_Animator.SetBool("isAttack", isAttack);
        m_Animator.SetBool("isClimbing", isClimbing);
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
        }
        else 
            JumpPlayer();
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
        targetVelocity = move * maxSpeed;
        if (isAttack)
            targetVelocity = Vector2.zero;
    }

    public void MoveLeft()
    {
        pastMove = Vector2.left * 0.7f;
        if (!isClimbJump)
            move = Vector2.left * 0.7f;
    }
    public void MoveRight()
    {
        pastMove = Vector2.right * 0.7f;
        if (!isClimbJump)
            move = Vector2.right * 0.7f;
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
                move = Vector2.left * 0.7f;
                
                if (!isJump)
                {
                    transform.Translate(Vector2.left * 0.1f);
                    JumpPlayer();
                    pastMove = -move;
                }
            }
            if (climbingFlip == true)
            {
                move = Vector2.right * 0.7f;
                if (!isJump)
                {
                    transform.Translate(Vector2.right * 0.1f);
                    JumpPlayer();
                    pastMove = -move;
                }
            }
            //isClimbJump = false;
        }
    }

    IEnumerator AttackedCoroutine()
    {
        isAttacked = true;
        
        for(int i = 0; i < 3; i++)
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
        if (isAttacked) return;
        GetComponent<PlayerInfo>().AddAttacked(damage);
        StartCoroutine(AttackedCoroutine());
        if (targetPos.y <= transform.position.y + 0.1f && velocity.y <= 0.0f)
            velocity.y = jumpTakeOffSpeed * 0.4f;

        mainCamera.GetComponent<CameraController>().ShakeCamera();
        //attackedMoveX = (transform.position.x - targetPos.x) * 0.25f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            GameObject obj = Instantiate(landingEffect, new Vector3(transform.position.x, transform.position.y - 0.45f), Quaternion.identity);
            Destroy(obj, 0.3f);
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
            if(grounded == false)
            {
                isClimbing = true;
                velocity.y = 0;
                isJump = false;
                climbingFlip = spriteRenderer.flipX;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isClimbing = false;
        }
    }
}
