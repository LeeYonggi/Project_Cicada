using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthWorm : MonoBehaviour {

    public int attackDamage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.GetChild(0).GetComponent<EarthWormRadar>().playerIsInView)
        {
            StartCoroutine(BobUp());
        }

	}
    
    IEnumerator BobUp()
    {

        yield return new WaitForSeconds(1);

    }
}
