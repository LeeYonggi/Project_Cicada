using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoarAttack : MonoBehaviour {

    public GameObject stone;
    public GameObject hugeBlock;
    public GameObject roundBlock;

    public GameObject throwingBlock;

    public List<GameObject> monsters;
    
    private bool approaching;

    private bool hugeBlockExist;
    private bool normalBlockExist;

    public float spreadingStoneTime;

    public float moveSpeed;
    private float fixedMoveSpeed;

    public int chargeDamage;
    public float chargeSpeed;
    private bool charging;

    private bool walled;

    public int uppercutDamage;
    private bool readyForUppercut;

    public Transform playerTrans;
    private float xDis;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {

        approaching = false;

        walled = false;

        readyForUppercut = false;

        fixedMoveSpeed = moveSpeed;

        rb = GetComponent<Rigidbody2D>();

        GetComponent<MonsterInfo>().basicMoving = false;


        GetComponent<MonsterInfo>().StartStun(0.1f);
    }
	
	// Update is called once per frame
	void Update () {
        
        if (!GetComponent<MonsterInfo>().stunned)
        {
            xDis = playerTrans.position.x - transform.position.x;
            
            if (Mathf.Abs(xDis) < 2.0f && approaching)
            {
                approaching = false;
                readyForUppercut = true;
            }

            if ((xDis < 0 && transform.localScale.x > 0) || (xDis > 0 && transform.localScale.x < 0))
            {
                if (playerTrans.position.y > -0.5f)
                {
                    if (GetComponent<HorizontalMonsterMove>().playerIsInBackView == false)
                    {
                        //Debug.Log("TurnBack!!");
                        StartCoroutine(GetComponent<HorizontalMonsterMove>().TurnBack(0.5f));
                    }
                }
            }

            if (!GetComponent<MonsterInfo>().attacking && !approaching)
            {
                GetComponent<MonsterInfo>().attacking = true;
                GetComponent<MonsterInfo>().basicMoving = false;

                int hp = GetComponent<MonsterInfo>().hp;
                if (hp > 0)
                    EarlyAttack();
                //else if (hp > 300)
                //    MiddleAttack();
                //else
                //    LastAttack();
            }
        }

	}

    private void OnCollisionStay2D(Collision2D col)
    {
        if (charging)
        {
            if (col.gameObject.CompareTag("Ground"))
            {
                if (((col.transform.position.x > transform.position.x) && (transform.localScale.x > 0)) || ((col.transform.position.x < transform.position.x) && (transform.localScale.x < 0)))
                {
                    charging = false;
                }
            }
        }
    }

    void EarlyAttack()
    {

        int rand = Random.Range(0, 10);

        if (playerTrans.position.y < -0.5f)
        {
            //GetComponent<MonsterInfo>().basicMoving = true;

            if (rand < 4 || readyForUppercut)
            {
                // Uppercut

                if (Mathf.Abs(xDis) < 2.0f)
                {
                    StartCoroutine(Uppercut());
                }
                else
                {
                    GetComponent<MonsterInfo>().basicMoving = true;
                    GetComponent<MonsterInfo>().attacking = false;
                    approaching = true;
                }
            }
            else if (rand > 3)
            {
                // Charges
                StartCoroutine(Charge());

            }
            else
            {
                Debug.Log("YOYOYOYO");
                StartCoroutine(SpreadStone());
            }
        }
        else
        {
            //Debug.Log("PlayerIsUp");
            GetComponent<MonsterInfo>().basicMoving = false;

            if (rand < 4)
            {
                StartCoroutine(BlockStamp());
            }
            else if (rand < 8)
            {
                StartCoroutine(ThrowBlock());
            }
            else
            {
                StartCoroutine(MonsterStamp());
            }
        }

    }

    void MiddleAttack()
    {
        int rand = Random.Range(0, 10);

        if (rand < 6 || readyForUppercut)
        {
            // Uppercut

            if (Mathf.Abs(xDis) < 2.0f)
            {
                StartCoroutine(Uppercut());
            }
            else
            {
                GetComponent<MonsterInfo>().basicMoving = true;
                GetComponent<MonsterInfo>().attacking = false;
                approaching = true;
            }
        }
        else if (rand < 9 && !charging)
        {
            // Charges
            StartCoroutine(Charge());
        }
        else
        {
            StartCoroutine(MonsterStamp());
        }



    }

    void LastAttack()
    {

    }


    void DropBlocks()
    {
        int _rand = Random.Range(3, 5);

        for (int i = 0; i < _rand; i++)
        {
            Instantiate(stone, new Vector3(Random.Range(-4.0f, 4.0f), 6, 0), Quaternion.identity);
        }

        if (Random.Range(0.0f, 1.0f) > 0.5f)
        {
            Instantiate(hugeBlock, new Vector3(Random.Range(-4.0f, 4.0f), 6, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(roundBlock, new Vector3(Random.Range(-4.0f, 4.0f), 6, 0), Quaternion.identity);
        }
    }

    void DropMonsters()
    {
        int _rand = Random.Range(2, 4);

        for (int i = 0; i < _rand; i++)
        {
            Instantiate(monsters[Random.Range(0, 3)], new Vector3(Random.Range(-4.0f, 4.0f), 6, 0), Quaternion.identity);
        }
    }

    void ThrowStone()
    {
        Debug.Log("ThrowStone");

        GameObject _stone = Instantiate(stone, transform.position, Quaternion.identity);
        _stone.GetComponent<Rigidbody2D>().AddForce(transform.up, ForceMode2D.Impulse);
            


    }
    
    IEnumerator Charge()
    {

        GetComponent<HorizontalMonsterMove>().turnBackEnabled = false;
        GetComponent<MonsterInfo>().basicMoving = false;
        int dir = GetComponent<HorizontalMonsterMove>().direction;
        charging = true;
        GetComponent<Animator>().SetBool("Hitted", false);
        GetComponent<Animator>().SetBool("Charge", true);

        yield return new WaitForSeconds(1.5f);

        GetComponent<Animator>().SetBool("Hitted", false);
        GetComponent<Animator>().SetBool("Charge", true);
        transform.GetChild(1).gameObject.SetActive(true);

        while (true)
        {
            dir = GetComponent<HorizontalMonsterMove>().direction;

            if (charging)
            {
                yield return new WaitForSeconds(0.1f);
                rb.AddForce(new Vector2(dir * chargeSpeed * Time.deltaTime, 0));
            }
            else
            {
                break;
            }

        }

        transform.GetChild(1).gameObject.SetActive(false);
        charging = false;
        GetComponent<Animator>().SetBool("Charge", false);

        //DropBlocks();
        GetComponent<MonsterInfo>().basicMoving = false;
        GetComponent<Animator>().SetBool("Move", false);
        yield return new WaitForSeconds(3.0f);
        GetComponent<Animator>().SetBool("Move", true);

        if (!GetComponent<MonsterInfo>().stunned)
        {
            GetComponent<HorizontalMonsterMove>().turnBackEnabled = true;
            GetComponent<MonsterInfo>().basicMoving = true;
            GetComponent<MonsterInfo>().attacking = false;
        }

        GetComponent<Animator>().SetBool("Hitted", false);
        StartCoroutine(GetComponent<HorizontalMonsterMove>().TurnBack(0.5f));
    }

    IEnumerator Uppercut()
    {
        GetComponent<MonsterInfo>().basicMoving = false;
        GetComponent<HorizontalMonsterMove>().turnBackEnabled = false;
        GetComponent<Animator>().SetBool("Uppercut", true);
        yield return new WaitForSeconds(0.3f);

        GetComponent<Animator>().SetBool("Uppercut", true);
        transform.GetChild(2).gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        transform.GetChild(2).gameObject.SetActive(false);
        readyForUppercut = false;
        GetComponent<Animator>().SetBool("Uppercut", false);
        if (!GetComponent<MonsterInfo>().stunned)
        {
            GetComponent<HorizontalMonsterMove>().turnBackEnabled = true;
            GetComponent<MonsterInfo>().basicMoving = true;
            GetComponent<MonsterInfo>().attacking = false;
        }

    }

    IEnumerator SpreadStone()
    {
        Debug.Log("SpreadStone Start");

        GetComponent<MonsterInfo>().basicMoving = false;
        GetComponent<HorizontalMonsterMove>().turnBackEnabled = false;

        yield return new WaitForSeconds(1.0f);

        float spreadStoneStartTime = Time.realtimeSinceStartup;
        float spreadStoneFrameTime = spreadStoneStartTime;
        float randomSpreadStoneTime = 0.0f;

        while (true)
        {
            if (Time.realtimeSinceStartup > spreadStoneStartTime + spreadingStoneTime)
            {
                break;
            }

            if (Time.realtimeSinceStartup >= spreadStoneFrameTime + randomSpreadStoneTime)
            {
                ThrowStone();

                spreadStoneFrameTime = Time.realtimeSinceStartup;
                randomSpreadStoneTime = Random.Range(0.3f, 0.5f);
            }
        }

        yield return new WaitForSeconds(1.0f);

        if (!GetComponent<MonsterInfo>().stunned)
        {
            GetComponent<HorizontalMonsterMove>().turnBackEnabled = true;
            GetComponent<MonsterInfo>().basicMoving = true;
            GetComponent<MonsterInfo>().attacking = false;
        }

        Debug.Log("SpreadStone Stop");
    }

    IEnumerator BlockStamp()
    {
        GetComponent<MonsterInfo>().attacking = true;
        GetComponent<HorizontalMonsterMove>().turnBackEnabled = false;
        GetComponent<MonsterInfo>().basicMoving = false;
        GetComponent<Animator>().SetBool("Uppercut", true);

        yield return new WaitForSeconds(1.0f);

        DropBlocks();

        yield return new WaitForSeconds(5.0f);

        GetComponent<Animator>().SetBool("Uppercut", false);
        if (!GetComponent<MonsterInfo>().stunned)
        {
            GetComponent<MonsterInfo>().attacking = false;
            GetComponent<HorizontalMonsterMove>().turnBackEnabled = true;
        }
    }

    IEnumerator MonsterStamp()
    {
        GetComponent<MonsterInfo>().attacking = true;
        GetComponent<HorizontalMonsterMove>().turnBackEnabled = false;
        GetComponent<MonsterInfo>().basicMoving = false;
        GetComponent<Animator>().SetBool("Uppercut", true);

        yield return new WaitForSeconds(1.0f);

        DropMonsters();

        yield return new WaitForSeconds(6.0f);

        GetComponent<Animator>().SetBool("Uppercut", false);
        if (!GetComponent<MonsterInfo>().stunned)
        {
            GetComponent<MonsterInfo>().attacking = false;
            GetComponent<HorizontalMonsterMove>().turnBackEnabled = true;
        }
    }

    IEnumerator ThrowBlock()
    {
        GetComponent<MonsterInfo>().attacking = true;
        GetComponent<HorizontalMonsterMove>().turnBackEnabled = false;
        GetComponent<MonsterInfo>().basicMoving = false;
        GetComponent<Animator>().SetBool("Uppercut", true);

        yield return new WaitForSeconds(0.5f);

        GameObject _throwingBlock = Instantiate(throwingBlock, transform.position, Quaternion.identity);
        _throwingBlock.GetComponent<ThrowingBlock>().playerPos = playerTrans.position;

        float z = Mathf.Atan2((transform.GetChild(0).GetComponent<MonsterView>().playerPos.y - _throwingBlock.transform.position.y),
            (transform.GetChild(0).GetComponent<MonsterView>().playerPos.x - _throwingBlock.transform.position.x)) * Mathf.Rad2Deg - 90;

        _throwingBlock.transform.eulerAngles = new Vector3(0, 0, z);

        yield return new WaitForSeconds(5.0f);

        GetComponent<Animator>().SetBool("Uppercut", false);
        if (!GetComponent<MonsterInfo>().stunned)
        {
            GetComponent<MonsterInfo>().attacking = false;
            GetComponent<HorizontalMonsterMove>().turnBackEnabled = true;
        }
    }

    //IEnumerator KickBlock(Transform blockTrans)
    //{
    //    if (blockTrans.GetComponent<BlockInfo>() != null)
    //    {
    //        attacking = true;
    //        kickingBlock = true;
    //        GetComponent<HorizontalMonsterMove>().turnBackEnabled = false;
    //        GetComponent<MonsterInfo>().basicMoving = false;

    //        yield return new WaitForSeconds(1.0f);

    //        blockTrans.GetComponent<Rigidbody2D>().AddForce(new Vector2(GetComponent<HorizontalMonsterMove>().direction * kickBlockSpeed * Time.deltaTime, 0), ForceMode2D.Impulse);
    //        blockTrans.GetComponent<BlockInfo>().durability -= 40;

    //        yield return new WaitForSeconds(0.5f);

    //        attacking = false;
    //        kickingBlock = false;
    //        GetComponent<HorizontalMonsterMove>().turnBackEnabled = true;
    //        GetComponent<MonsterInfo>().basicMoving = true;
    //    }
    //}



}
