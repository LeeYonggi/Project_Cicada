﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : MonoBehaviour {

    [HideInInspector] public int maxHp;
    [HideInInspector] public int hp;
    [HideInInspector] public bool dead;
    public bool invincible;
    [HideInInspector] public bool basicMoving;
    [HideInInspector] public bool attacking;
    [HideInInspector] public bool stunned;
    private bool playerIsInView;
    [HideInInspector] public bool playerRecognized;


    public float loseRecogOfPlayerTime;

    public float dyingDur;

    public bool landMonster;

    public GameObject hittedEffect;

    private AudioSource idleSound;
    private AudioSource dieSound;

    private Vector3 initialPos;


    private void Awake()
    {
        Debug.Log("InitialPos : " + initialPos);
        initialPos = transform.position;
    }

    // Use this for initialization
    void Start () {

        maxHp = hp;

        if (playerIsInView)
            playerRecognized = true;

        invincible = false;
        dead = false;
        
        attacking = false;
        if (landMonster) basicMoving = false;
        else basicMoving = true;
        stunned = false;
        
        idleSound = GetComponents<AudioSource>()[0];
        dieSound = GetComponents<AudioSource>()[1];

        if (dyingDur <= 0) dyingDur = 0.5f;
    }
	
	// Update is called once per frame
	void Update () {

        if (dead) return;

        if (hp <= 0 && !dead)
        {
            hp = 0;
            //Die();
        }

        if (basicMoving)
        {
            GetComponent<Animator>().SetBool("Move", true);
            if (!idleSound.isPlaying) idleSound.Play();
        }
        else
        {
            GetComponent<Animator>().SetBool("Move", false);
            if (idleSound.isPlaying) idleSound.Stop();
        }
	}

    private void OnEnable()
    {
        attacking = false;
        basicMoving = true;
        dead = false;
        stunned = false;

        Debug.Log("transform.position : " + transform.position);
        transform.position = initialPos;
    }

    public bool GetPlayerIsInView()
    {
        return playerIsInView;
    }

    public bool GetPlayerRecognized()
    {
        return playerRecognized;
    }

    public void SeePlayer()
    {
        playerIsInView = true;
        playerRecognized = true;
    }

    public void MissPlayer()
    {
        playerIsInView = false;
        StartCoroutine(LoseRecogOfPlayer());
    }

    IEnumerator LoseRecogOfPlayer()
    {
        yield return new WaitForSeconds(loseRecogOfPlayerTime);

        playerRecognized = false;
    }

    public void StartStun(float dur)
    {
        StartCoroutine(Stun(dur));
    }

    public IEnumerator BossMonsterHitted(float dur)
    {
        //bool tempAttacking = GetComponent<Animator>().GetBool("Uppercut");
        //GetComponent<Animator>().SetBool("Uppercut", false);
        GetComponent<Animator>().SetBool("Hitted", true);

        yield return new WaitForSeconds(dur);
        
        GetComponent<Animator>().SetBool("Hitted", false);
        //if (tempAttacking)
        //    GetComponent<Animator>().SetBool("Uppercut", true);
    }

    private IEnumerator Stun(float dur)
    {
        if (!stunned)
        {
            stunned = true;
            GetComponent<Animator>().SetBool("Hitted", true);

            yield return new WaitForSeconds(dur);
            
            stunned = false;
            attacking = false;
            GetComponent<Animator>().SetBool("Hitted", false);
        }
    }

    private IEnumerator _Die()
    {
        dead = true;
        GetComponent<Animator>().SetBool("Die", true);
        Instantiate(hittedEffect, transform.position, Quaternion.identity);
        dieSound.Play();

        yield return new WaitForSeconds(dyingDur);

        GetComponent<Animator>().SetBool("Die", false);
        gameObject.SetActive(false);
        //GameObject obj = GameObject.FindGameObjectWithTag("Player");
        //obj.GetComponent<PlayerState>().AbilityState = (AbilityState)monsterState;

    }

    public void Die()
    {
        if (!invincible)
            StartCoroutine(_Die());
    }
}
