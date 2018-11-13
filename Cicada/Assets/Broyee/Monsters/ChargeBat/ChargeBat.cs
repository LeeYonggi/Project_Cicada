using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBat : MonoBehaviour {

    public float chargeSpeed;
    private bool charging;

    private Rigidbody2D rb;

    private AudioSource chargeSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        chargeSound = GetComponents<AudioSource>()[2];
    }

    private void Update()
    {
        if (GetComponent<MonsterInfo>().dead) return;

        if (GetComponent<MonsterInfo>().GetPlayerIsInView() && !charging)
        {
            StartCoroutine(Charge());
        }
    }

    private void OnEnable()
    {
        charging = false;
    }

    private IEnumerator Charge()
    {
        Debug.Log("Charged");
        charging = true;
        GetComponent<MonsterInfo>().basicMoving = false;
        GetComponent<HorizontalMonsterMove>().LookAtPlayer();
        GetComponent<HorizontalMonsterMove>().turnBackEnabled = false;
        GetComponent<Animator>().SetBool("Attack", true);
        chargeSound.Play();

        yield return new WaitForSeconds(0.7f);

        rb.AddForce((transform.GetChild(0).GetComponent<MonsterView>().playerPos - transform.position).normalized * Time.deltaTime * chargeSpeed, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.7f);

        rb.velocity = new Vector2(0, 0);
        GetComponent<Animator>().SetBool("Attack", false);

        GetComponents<AudioSource>()[0].Play();
        yield return new WaitForSeconds(2);
        GetComponent<MonsterInfo>().basicMoving = true;
        GetComponent<HorizontalMonsterMove>().turnBackEnabled = true;
        charging = false;
    }

}

