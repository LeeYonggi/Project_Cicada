﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantisAttack : MonoBehaviour {

    public int shortAttackDamage;
    public int longAttackDamage;

    private bool longAttacking;

    private int atkDir;
    
	
	// Update is called once per frame
	void Update () {

        if (GetComponent<MonsterInfo>().stunned)
        {
            GetComponent<Animator>().SetBool("ShortAttack", false);
            transform.GetChild(1).gameObject.SetActive(false);
            StopAllCoroutines();
        }
        else
        {
            if (GetComponent<MonsterInfo>().GetPlayerIsInView() && !GetComponent<MonsterInfo>().attacking)
            {
                GetComponent<MonsterInfo>().attacking = true;
                GetComponent<MonsterInfo>().basicMoving = false;

                Vector3 playerPos = transform.GetChild(0).gameObject.GetComponent<MonsterView>().playerPos;
                if (System.Math.Abs(transform.position.x - playerPos.x) < 1.5f)
                    StartCoroutine(ShortAttack());
                else StartCoroutine(LongAttack());
            }

            if (longAttacking)
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
                        //obj.GetComponent<PlayerTrap>().Hurt(longAttackDamage);
                    }
                }
            }
        }
	}

    IEnumerator ShortAttack()
    {
        GetComponent<Animator>().SetBool("ShortAttack", true);
        yield return new WaitForSeconds(0.3f);

        //atkDir = GetComponent<HorizontalMonsterMove>().direction;
        //transform.GetChild(1).position = new Vector3(transform.position.x + atkDir, transform.position.y, 0);
        //Vector3 atkEffectRot = new Vector3(0, 0, 0);
        //if (transform.GetChild(1).position.x < 0)
        //    atkEffectRot.z = 180;
        //transform.GetChild(1).Rotate(atkEffectRot);
        transform.GetChild(1).gameObject.SetActive(true);

        yield return new WaitForSeconds(0.2f);
        transform.GetChild(1).gameObject.SetActive(false);
        GetComponent<Animator>().SetBool("ShortAttack", false);
        
        
        Vector2 attackPos = new Vector2(transform.position.x, transform.position.y + (transform.lossyScale.y / 2));
        Vector2 attackRange = new Vector2(1.5f, transform.lossyScale.y);
        int angle = 0;
        if (atkDir < 0) angle = 180;

        GameObject obj;
        if (Physics2D.OverlapBox(attackPos, attackRange, angle) != null)
        {
            obj = Physics2D.OverlapBox(attackPos, attackRange, angle).gameObject;
            if (obj.CompareTag("Player"))
            {
                //obj.GetComponent<PlayerTrap>().Hurt(longAttackDamage);
            }
        }

        GetComponent<MonsterInfo>().attacking = false;
        GetComponent<MonsterInfo>().basicMoving = true;
    }

    IEnumerator LongAttack()
    {
        GetComponent<Animator>().SetBool("Move", true);        
        yield return new WaitForSeconds(0.7f);
        atkDir = GetComponent<HorizontalMonsterMove>().direction;
        longAttacking = true;
        for (int i = 0; i < 30; i++)
        {
            transform.Translate(5.0f * Time.deltaTime * atkDir, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        GetComponent<Animator>().SetBool("Move", false);
        longAttacking = false;
        
        Vector2 attackPos = new Vector2(transform.position.x, transform.position.y + (transform.lossyScale.y / 2));
        Vector2 attackRange = new Vector2(transform.lossyScale.x / 2, transform.lossyScale.y);
        int angle = 0;
        if (atkDir < 0) angle = 180;

        GameObject obj;
        if (Physics2D.OverlapBox(attackPos, attackRange, angle) != null)
        {
            obj = Physics2D.OverlapBox(attackPos, attackRange, angle).gameObject;
            if (obj.CompareTag("Player"))
            {
                Debug.Log("Long Attack hitted");
                //obj.GetComponent<PlayerTrap>().Hurt(longAttackDamage);
            }
        }

        GetComponent<MonsterInfo>().attacking = false;
        GetComponent<MonsterInfo>().basicMoving = true;
    }

    
}
