using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour {

    public bool fromAbove;
    public bool generatedFromRock;

    public int damage;

	// Use this for initialization
	void Awake () {

        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            //col.GetComponent<PlayerTrap>().Hurt(damage);
        }
        else if (fromAbove || generatedFromRock)
        {
            if (col.CompareTag("Monster") || col.CompareTag("BossMonster"))
            {
                col.GetComponent<MonsterInfo>().hp -= damage;

                Destroy(gameObject);
            }
        }

        if (fromAbove)
            Destroy(gameObject);
        else if (generatedFromRock)
        {
            if (col.name == "Ground")
            {
                //Debug.Log("Destroyed by " + col.name);
                Destroy(gameObject);
            }
        }
        else
        {
            if (!col.CompareTag("BossMonster"))
            {
                //Debug.Log("Destroyed by " + col.name);
                Destroy(gameObject);
            }
        }

    }
}
