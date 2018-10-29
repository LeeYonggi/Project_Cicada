﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolyPolyRoll : MonoBehaviour
{

    public int rollDamage;
    public float rollSpeed;
    private float basicSpeed;

    private bool rolling;

    void Start () {

        basicSpeed = GetComponent<HorizontalMonsterMove>().moveSpeed;
    }
    

    void Update()
    {

        if (GetComponent<MonsterInfo>().GetPlayerIsInView() && !rolling)
        {
            StartCoroutine(RollStart());
        }

    }


    IEnumerator RollStart()
    {
        rolling = true;
        GetComponent<Animator>().SetBool("RollStart", true);

        yield return new WaitForSeconds(1.0f);

        GetComponent<Animator>().SetBool("RollStart", false);
        GetComponent<Animator>().SetBool("Roll", true);
        transform.GetChild(1).gameObject.SetActive(true);

        GetComponent<HorizontalMonsterMove>().moveSpeed = rollSpeed;
        GetComponent<MonsterInfo>().basicMoving = true;
    }

    IEnumerator RollStop()
    {
        GetComponent<Animator>().SetBool("Roll", false);
        GetComponent<Animator>().SetBool("RollStop",true);
        transform.GetChild(1).gameObject.SetActive(false);

        GetComponent<MonsterInfo>().basicMoving = false;

        yield return new WaitForSeconds(1.0f);

        GetComponent<Animator>().SetBool("RollStop", false);
        GetComponent<Animator>().SetBool("Move", true);

        rolling = false;
        GetComponent<HorizontalMonsterMove>().moveSpeed = basicSpeed;
        GetComponent<MonsterInfo>().basicMoving = true;
    }
}
