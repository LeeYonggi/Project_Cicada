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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<MonsterInfo>().dead) return;

        if (GetComponent<MonsterInfo>().GetPlayerIsInView() && !GetComponent<MonsterInfo>().attacking)
        {
            if (GetComponent<HorizontalMonsterMove>().LookAtPlayer())
                transform.Translate(-0.3f * transform.localScale.x, 0, 0);
            StartCoroutine(PeaShoot());
        }
    }

    IEnumerator PeaShoot()
    {
        GetComponent<MonsterInfo>().attacking = true;
        GetComponent<Animator>().SetBool("Attack", true);
        GetComponents<AudioSource>()[2].Play();
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


            //Debug.Log("Pea EulerAngles : " + z.ToString());
        }

        tempPea.transform.eulerAngles = new Vector3(0, 0, z);

        yield return new WaitForSeconds(peaShootRate);

        GetComponent<MonsterInfo>().attacking = false;
        GetComponent<Animator>().SetBool("Attack", false);
    }
}
