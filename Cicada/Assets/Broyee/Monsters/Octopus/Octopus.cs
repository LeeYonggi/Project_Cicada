using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : MonoBehaviour {

    public bool horizontallyShoot;

    private int atkDir;

    public float inkShootRate;

    public GameObject ink;

    private bool previousBasicMoving;

    // Use this for initialization
    void Start()
    {
        previousBasicMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<MonsterInfo>().dead) return;

        if (GetComponent<MonsterInfo>().GetPlayerIsInView() && !GetComponent<MonsterInfo>().attacking)
        {
            if (GetComponent<HorizontalMonsterMove>().LookAtPlayer())
                transform.Translate(-0.3f * transform.localScale.x, 0, 0);
            StartCoroutine(InkShoot());
        }


        if (GetComponent<MonsterInfo>().basicMoving != previousBasicMoving)
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;

        previousBasicMoving = GetComponent<MonsterInfo>().basicMoving;
    }

    IEnumerator InkShoot()
    {
        GetComponent<MonsterInfo>().attacking = true;
        GetComponent<MonsterInfo>().basicMoving = false;
        //GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        GetComponent<Animator>().SetBool("Attack", true);
        GetComponents<AudioSource>()[2].Play();
        yield return new WaitForSeconds(0.3f);

        Vector3 tempPos = new Vector3(transform.position.x, transform.position.y, 0);

        GameObject tempPea = Instantiate(ink, tempPos, Quaternion.identity);

        float z = 0.0f;

        if (horizontallyShoot)
        {
            if (tempPos.x < transform.GetChild(0).GetComponent<MonsterView>().playerPos.x)
                z = -90.0f;
            else
                z = -270.0f;
        }
        else
        {
            z = Mathf.Atan2((transform.GetChild(0).GetComponent<MonsterView>().playerPos.y - tempPea.transform.position.y),
                (transform.GetChild(0).GetComponent<MonsterView>().playerPos.x - tempPea.transform.position.x)) * Mathf.Rad2Deg - 90;


            //Debug.Log("Pea EulerAngles : " + z.ToString());
        }

        tempPea.transform.eulerAngles = new Vector3(0, 0, z);

        yield return new WaitForSeconds(inkShootRate);

        GetComponent<MonsterInfo>().attacking = false;
        GetComponent<MonsterInfo>().basicMoving = true;
        //GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        GetComponent<Animator>().SetBool("Attack", false);
    }
}
