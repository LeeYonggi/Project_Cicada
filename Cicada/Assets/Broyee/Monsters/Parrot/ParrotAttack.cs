using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrotAttack : MonoBehaviour {

    private Rigidbody2D rb;

    private int atkDir;

    public int chargeDamage;

    public float chargeSpeed;
    public float chaseSpeed;

    private bool charging;

    private bool prev_stunned;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();

        atkDir = GetComponent<HorizontalMonsterMove>().direction;

        GetComponent<MonsterInfo>().basicMoving = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (GetComponent<MonsterInfo>().stunned)
        {
            StopAllCoroutines();

            if (!charging)
                rb.velocity = Vector2.zero;
        }
        else
        {
            if (prev_stunned)
            {
                StopCoroutine(Chase());
                StartCoroutine(Chase());
            }

            if (GetComponent<MonsterInfo>().GetPlayerIsInView() && !charging)
            {
                charging = true;
                GetComponent<MonsterInfo>().basicMoving = false;
                GetComponent<BirdFly>().flying = false;
                StartCoroutine(Charge());
            }

            if (charging)
            {
                Vector2 attackPos = new Vector2(transform.position.x, transform.position.y);
                Vector2 attackRange = new Vector2(transform.lossyScale.x / 2, transform.lossyScale.y);
                int angle = 0;
                if (atkDir < 0) angle = 180;

                GameObject obj;
                if (Physics2D.OverlapBox(attackPos, attackRange, angle) != null)
                {
                    obj = Physics2D.OverlapBox(attackPos, attackRange, angle).gameObject;
                    if (obj.CompareTag("Player"))
                    {
                        //obj.GetComponent<PlayerTrap>().Hurt(chargeDamage);
                    }
                }
            }
        }

        prev_stunned = GetComponent<MonsterInfo>().stunned;
    }

    IEnumerator Charge()
    {
        Debug.Log("Charged");
        GetComponent<MonsterInfo>().basicMoving = false;
        GetComponent<Animator>().SetBool("Attack", true);

        yield return new WaitForSeconds(0.5f);

        rb.AddForce((transform.GetChild(0).GetComponent<MonsterView>().playerPos - transform.position).normalized * Time.deltaTime * chargeSpeed, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.5f);
        GetComponent<Animator>().SetBool("Attack", false);

        StartCoroutine(Chase());
    }

    IEnumerator Chase()
    {
        if (!GetComponent<MonsterInfo>().stunned)
        {
            rb.velocity = Vector2.zero;
            yield return new WaitForSeconds(0.2f);

            for (int i = 0; i < 200; i++)
            {
                Debug.Log("Chasing");
                if (GetComponent<MonsterInfo>().stunned)
                {
                    Debug.Log("Stunned while chasing");
                    yield break;
                }

                Vector3 target = transform.GetChild(0).GetComponent<MonsterView>().playerPos;
                target.x += Random.Range(-1.0f, 1.0f);
                target.y += 0.5f;

                rb.AddForce((target - transform.position) * Time.deltaTime * chaseSpeed);

                if (rb.velocity.x > 0) GetComponent<HorizontalMonsterMove>().direction = 1;
                else if (rb.velocity.x < 0) GetComponent<HorizontalMonsterMove>().direction = -1;

                yield return new WaitForSeconds(0.01f);
            }

            yield return new WaitForSeconds(0.3f);

            GetComponent<MonsterInfo>().basicMoving = true;
            GetComponent<BirdFly>().flying = true;
            charging = false;
        }
    }


}
