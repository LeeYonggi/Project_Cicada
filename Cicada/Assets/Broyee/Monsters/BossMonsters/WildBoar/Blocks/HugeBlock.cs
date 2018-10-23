using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HugeBlock : MonoBehaviour {

    public GameObject stone;

    public int durability;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (durability <= 0)
        {
            int rand = Random.Range(3, 5);
            for (int i = 0; i < rand; i++)
            {
                Vector3 tempVec = transform.position;
                tempVec.x += Random.Range(-1.0f, 1.0f);
                tempVec.y += Random.Range(-1.0f, 1.0f);
                Instantiate(stone, tempVec, Quaternion.identity).GetComponent<Stone>().generatedFromRock = true;
            }

            Destroy(gameObject);
        }
        
	}
}
