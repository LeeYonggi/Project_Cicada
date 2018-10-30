using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemAttack : MonoBehaviour {

    public int attackDamage;
    public float sneakRange;

    private bool camouflaged;
    private bool defending;
    private bool attacking;
    private bool approaching;

	// Use this for initialization
	void Start () {
        approaching = false;
        camouflaged = true;

        StartCoroutine(Defend(9999.0f));
	}

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Mathf.Abs(transform.position.x - transform.GetChild(0).GetComponent<MonsterView>().playerPos.x);

        Sneak();
        Camouflage();

        if (distanceFromPlayer < sneakRange && approaching)
        {
            approaching = false;
            StartCoroutine(Attack());
        }

        if (!attacking && !defending && !camouflaged && !approaching)
        {
            bool act = false;
            if (Random.Range(-1.0f, 1.0f) < 0) act = true;

            if (act)
            {
                if (distanceFromPlayer <= sneakRange)
                {
                    StartCoroutine(Attack());
                }
                else approaching = true;
            }
            else
            {
                StartCoroutine(Defend(Random.Range(2.0f, 3.0f)));
            }
        }

        bool invincible = false;
        if (defending)
        {
            GetComponent<MonsterInfo>().basicMoving = false;

            if ((transform.position.x - transform.GetChild(0).GetComponent<MonsterView>().playerPos.x > 0) && transform.localScale.x < 0)
                invincible = true;
            else if ((transform.position.x - transform.GetChild(0).GetComponent<MonsterView>().playerPos.x < 0) && transform.localScale.x > 0)
                invincible = true;

            invincible = true;
            
            GetComponent<MonsterInfo>().invincible = invincible;
        }
    }


    private void Sneak()
    {
        if (GetComponent<MonsterInfo>().playerRecognized && camouflaged)
        {
            if (Mathf.Abs(transform.position.x - transform.GetChild(0).GetComponent<MonsterView>().playerPos.x) < sneakRange)
            {                

                StopCoroutine(Defend(9999.0f));

                camouflaged = false;
                defending = false;                
                GetComponent<MonsterInfo>().basicMoving = true;
                GetComponent<Animator>().SetBool("Defend", false);
                GetComponent<Animator>().SetBool("Undefend", true);

                StartCoroutine(Attack());
            }
        }
    }

    private void Camouflage()
    {
        if (!GetComponent<MonsterInfo>().playerRecognized)
        {
            camouflaged = true;
            StartCoroutine(Defend(9999.0f));
        }
    }

    private IEnumerator Attack()
    {
        //Debug.Log("Attack");

        attacking = true;
        GetComponent<MonsterInfo>().basicMoving = false;
        GetComponent<Animator>().SetBool("Attack", true);

        yield return new WaitForSeconds(0.3f);

        transform.GetChild(1).gameObject.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        transform.GetChild(1).gameObject.SetActive(false);

        attacking = false;
        GetComponent<MonsterInfo>().basicMoving = true;
        GetComponent<Animator>().SetBool("Attack", false);
    }

    private IEnumerator Defend(float time)
    {
        //Debug.Log("Defend");

        defending = true;
        GetComponent<MonsterInfo>().basicMoving = false;
        GetComponent<Animator>().SetBool("Defend", true);
        GetComponent<Animator>().SetBool("Undefend", false);

        yield return new WaitForSeconds(time);

        GetComponent<Animator>().SetBool("Defend", false);
        GetComponent<Animator>().SetBool("Undefend", true);

        yield return new WaitForSeconds(0.5f);

        defending = false;
        GetComponent<MonsterInfo>().basicMoving = true;
        GetComponent<Animator>().SetBool("Undefend", false);
    }
}
