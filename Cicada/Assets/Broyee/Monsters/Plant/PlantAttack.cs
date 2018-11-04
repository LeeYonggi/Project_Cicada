using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAttack : MonoBehaviour {

    public bool horizontallyShoot;

    private int atkDir;    

    public float peaShootRate;

    public GameObject pea;

	// Use this for initialization
	void Start () {
        transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
    }
	
	// Update is called once per frame
	void Update () {

        if (!GetComponent<MonsterInfo>().stunned)
        {
            if (GetComponent<HorizontalMonsterMove>().grounded)
            {
                if (GetComponent<MonsterInfo>().GetPlayerIsInView() && !GetComponent<MonsterInfo>().attacking)
                {
                    GetComponent<MonsterInfo>().basicMoving = false;
                    StartCoroutine(PeaShoot());
                }
                if (GetComponent<MonsterInfo>().GetPlayerIsInView())
                    GetComponent<MonsterInfo>().basicMoving = false;
                else GetComponent<MonsterInfo>().basicMoving = true;
            }
        }
        else
        {
            StopCoroutine(PeaShoot());
            GetComponent<MonsterInfo>().attacking = false;
            GetComponent<Animator>().SetBool("Attack", false);
        }
    }

    IEnumerator PeaShoot()
    {
        GetComponent<MonsterInfo>().attacking = true;
        GetComponent<Animator>().SetBool("Attack", true);
        yield return new WaitForSeconds(0.3f);

        Vector3 tempPos = new Vector3(transform.position.x, transform.position.y, 0);
        
        GameObject tempPea = Instantiate(pea, tempPos, Quaternion.identity);

        float z = 0.0f;

        if (horizontallyShoot)
        {
            if (tempPos.x < transform.GetChild(0).GetComponent<MonsterView>().playerPos.x)
                z = - 90.0f;
            else
                z = - 270.0f;
        }
        else
        {
            z = Mathf.Atan2((transform.GetChild(0).GetComponent<MonsterView>().playerPos.y - tempPea.transform.position.y),
                (transform.GetChild(0).GetComponent<MonsterView>().playerPos.x - tempPea.transform.position.x)) * Mathf.Rad2Deg - 90;


            Debug.Log("Pea EulerAngles : " + z.ToString());
        }

        tempPea.transform.eulerAngles = new Vector3(0, 0, z);

        yield return new WaitForSeconds(peaShootRate);

        GetComponent<MonsterInfo>().attacking = false;
        GetComponent<Animator>().SetBool("Attack", false);
    }
}
