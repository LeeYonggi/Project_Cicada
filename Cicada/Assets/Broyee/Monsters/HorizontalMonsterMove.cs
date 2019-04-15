using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMonsterMove : MonoBehaviour {

    [HideInInspector] public bool turnBackEnabled;

    public int direction;
    // Alterable movement speed
    public float moveSpeed;
    [HideInInspector] public float fixedMoveSpeed;

    // When it is true check whether it is possible to move then move
    public bool moveCheckEnabled;

    // Used for adjust little collider miss
    public float colDis;

    [HideInInspector] public bool grounded;

    // True when this monster is turning
    private bool turningBack;

    // True when player is behind of itself
    [HideInInspector] public bool playerIsInBackView;
    // Distance of view when it see behind
    public float backViewDis;

    // normaly used for flying monsters 
    public float groundCheckHeight;

    public float turnBackTime;
    
	void Start () {

        moveCheckEnabled = true;
        grounded = false;
        turningBack = false;
        turnBackEnabled = true;
        playerIsInBackView = false;

        if (transform.localScale.x > 0) direction = 1;
        else direction = -1;
    }

    private void OnEnable()
    {
        moveCheckEnabled = true;
        grounded = false;
        turningBack = false;
        turnBackEnabled = true;
        playerIsInBackView = false;
    }
    
    void Update () {

        if (GetComponent<MonsterInfo>().dead) return;
        
        if (!GetComponent<MonsterInfo>().stunned)
        {
            // Move
            if (GetComponent<MonsterInfo>().basicMoving)
            {
                if (!turningBack)
                {
                    if (moveCheckEnabled)
                    {
                        if (MoveCheck())
                        {
                            GetComponent<Animator>().SetBool("Hitted", false);
                            transform.Translate(moveSpeed * direction * Time.deltaTime, 0, 0);
                        }
                        else
                        {
                            StartCoroutine(TurnBack(0.0f));
                        }
                    }
                    else
                    {
                        GetComponent<Animator>().SetBool("Hitted", false);
                        transform.Translate(moveSpeed * direction * Time.deltaTime, 0, 0);
                    }
                }
            }

            // Turn back
            playerIsInBackView = false;

            if (!GetComponent<MonsterInfo>().GetPlayerIsInView() && GetComponent<MonsterInfo>().GetPlayerRecognized() && !turningBack)
            {
                Vector2 backColStart = new Vector2(transform.position.x - (Mathf.Abs(transform.lossyScale.x) / 2 * direction), transform.position.y + transform.lossyScale.y / 2);
                Vector2 backColEnd = new Vector2(backColStart.x - (backViewDis * direction), backColStart.y - transform.lossyScale.y);
                
                if (Physics2D.OverlapArea(backColStart, backColEnd) != null)                
                {
                    Collider2D[] col;
                    col = Physics2D.OverlapAreaAll(backColStart, backColEnd);
                    for (int i = 0; i < col.Length; i++)
                    {
                        if (col[i].CompareTag("Player"))
                        // if player is detected behind of it
                        {
                            playerIsInBackView = true;
                            StartCoroutine(TurnBack(turnBackTime));
                            break;
                        }
                    }
                }

            }
        }
    }

    public bool MoveCheck()
    {
        // Vector2s for collision check of falling
        Vector2 fallColStart = new Vector2(transform.position.x + (transform.lossyScale.x / 2 - (colDis * direction)), transform.position.y - transform.lossyScale.y / 2 + colDis);
        Vector2 fallColEnd = new Vector2(fallColStart.x + 0.001f, fallColStart.y - groundCheckHeight);
        // Vector2s for checking collision with wall
        Vector2 wallColStart = new Vector2(fallColStart.x, transform.position.y + (transform.lossyScale.y / 2));
        Vector2 wallColEnd = new Vector2(wallColStart.x + 0.001f, wallColStart.y - transform.lossyScale.y / 2);

        // Wall check
        if (Physics2D.OverlapArea(wallColStart, wallColEnd) != null)
        {
            Collider2D[] wallObj = Physics2D.OverlapAreaAll(wallColStart, wallColEnd);
            for (int i = 0; i < wallObj.Length; i++)
            {
                if (wallObj[i].CompareTag("Ground"))
                {
                    return false;
                }
            }
        }

        // Falling check
        if (!GetComponent<MonsterInfo>().landMonster) return true;
        if (Physics2D.OverlapArea(fallColStart, fallColEnd) != null)
        {
            Collider2D[] groundObj = Physics2D.OverlapAreaAll(fallColStart, fallColEnd);
            for (int i = 0; i < groundObj.Length; i++)
            {
                if (groundObj[i].CompareTag("Ground"))
                {
                    return true;
                }
            }
        }
                
        return false;
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || GetComponent<MonsterInfo>().landMonster)
        {
            grounded = true;
            GetComponent<MonsterInfo>().basicMoving = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || GetComponent<MonsterInfo>().landMonster)
        {
            grounded = false;
        }
    }
    
    public IEnumerator TurnBack(float time)
    {
        if (!GetComponent<MonsterInfo>().stunned)
        {
            if (turnBackEnabled)
            {
                GetComponent<Animator>().SetBool("Hitted", false);
                turningBack = true;

                yield return new WaitForSeconds(time);

                GetComponent<Animator>().SetBool("Hitted", false);
                turningBack = false;
                direction = -direction;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
            }
        }
    }

    public bool LookAtPlayer()
    {
        // if this monster is not looking at player
        if (direction != transform.GetChild(0).GetComponent<MonsterView>().PlayerIsOnRight())
        {
            StartCoroutine(TurnBack(0.0f));
            return true;
        }
        return false;
    }
}
