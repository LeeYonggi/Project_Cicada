using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMonsterMove : MonoBehaviour {

    [HideInInspector] public bool turnBackEnabled;

    public int direction;
    public float moveSpeed;
    [HideInInspector] public float fixedMoveSpeed;

    public float colDis;

    [HideInInspector] public bool grounded;

    private bool turningBack;

    [HideInInspector] public bool playerIsInBackView;
    public float backViewDis;

    public float groundCheckHeight;

    public float turnBackTime;

    public GameObject _fallColStart;
    public GameObject _fallColEnd;

    //public float climbableHeight;
    //public float fallableHeight;

	// Use this for initialization
	void Start () {
        grounded = false;

        turningBack = false;

        turnBackEnabled = true;

        playerIsInBackView = false;

    }
	
	// Update is called once per frame
	void Update () {

        if (!GetComponent<MonsterInfo>().stunned)
        {
            if (GetComponent<MonsterInfo>().basicMoving)
            {
                //Debug.Log("BasicMoving on");
                if (!turningBack)
                {
                    if (GetComponent<MonsterInfo>().landMonster)
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
                //else if (!turningBack)
                //{
                //    StartCoroutine(TurnBack(0.0f));
                //}
            }
            else Debug.Log("BasicMoving off");


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

        //Debug.Log("Walled : " + walled.ToString());
    }

    public bool MoveCheck()
    {
        if (!GetComponent<MonsterInfo>().landMonster) return true;

        Vector2 fallColStart = new Vector2(transform.position.x + (transform.lossyScale.x / 2 - (colDis * direction)), transform.position.y - transform.lossyScale.y / 2 + colDis);
        Vector2 fallColEnd = new Vector2(fallColStart.x + 0.001f, fallColStart.y - groundCheckHeight);

        Vector2 wallColStart = new Vector2(fallColStart.x, transform.position.y + (transform.lossyScale.y / 2));
        Vector2 wallColEnd = new Vector2(wallColStart.x + 0.001f, wallColStart.y - transform.lossyScale.y / 2);

        Instantiate(_fallColStart, wallColStart, Quaternion.identity);
        Instantiate(_fallColEnd, wallColEnd, Quaternion.identity);

        if (Physics2D.OverlapArea(wallColStart, wallColEnd, 0) != null)
        {
            Collider2D[] wallObj = Physics2D.OverlapAreaAll(wallColStart, wallColEnd);
            for (int i = 0; i < wallObj.Length; i++)
            {
                Debug.Log("WallObj tag : " + wallObj[i].tag);
                if (wallObj[i].CompareTag("Ground"))
                {
                    return false;
                }
            }
        }

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

        Debug.Log("Move Check returned false");
        return false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || GetComponent<MonsterInfo>().landMonster)
        {
            grounded = true;
            GetComponent<MonsterInfo>().basicMoving = true;
        }
        //if (col.gameObject.CompareTag("Ground"))
        //{
        //    if (turnBackEnabled)
        //    {
        //        if (gameObject.CompareTag("BossMonster") && col.gameObject.CompareTag("Block"))
        //            return;

        //        StartCoroutine(TurnBack(0.0f));
        //    }
        //}
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
                //Debug.Log("LiterallyTurnBack");
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
}
