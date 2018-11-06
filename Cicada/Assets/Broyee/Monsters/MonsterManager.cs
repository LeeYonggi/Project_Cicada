using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour {

    public float respawnTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator Respawn(GameObject monster)
    {

        yield return new WaitForSeconds(respawnTime);

        monster.SetActive(true);
    }
}
