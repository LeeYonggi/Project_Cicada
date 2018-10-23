using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundBlock : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("BossMonster"))
        {
            Debug.Log("RoundBlockCollided");
            col.gameObject.GetComponent<MonsterInfo>().StartStun(5.0f);
        }
    }
}
