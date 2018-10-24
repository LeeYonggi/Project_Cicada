using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthWorm : MonoBehaviour {

    private int dir;
    private bool attacking;
    private float colXPos;

	// Use this for initialization
	void Start () {
        attacking = false;

        colXPos = transform.GetChild(0).position.x;
	}
	
	// Update is called once per frame
	void Update () {

        dir = 1;

        if (transform.localScale.x < 0) dir = -1;

        if (transform.GetChild(0).GetComponent<EarthWormRadar>().playerIsInView && !attacking)
        {
            StartCoroutine(BobUp());
        }

	}
    
    IEnumerator BobUp()
    {
        Debug.Log("Bob Up");

        attacking = true;

        yield return new WaitForSeconds(0.5f);

        GetComponent<Animator>().SetBool("BobUp", true);

        while (transform.GetChild(1).position.x < (colXPos + 0.3f) * dir)
        {
            transform.GetChild(1).Translate(5.0f * Time.deltaTime * dir, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }

        GetComponent<Animator>().SetBool("BobUp", false);
        GetComponent<Animator>().SetBool("BobDown", true);

        Debug.Log("Bob Down");

        while (transform.GetChild(1).position.x > (colXPos - (0.3f * dir)) * dir)
        {
            transform.GetChild(1).Translate(-5.0f * Time.deltaTime * dir, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }

        attacking = false;
        GetComponent<Animator>().SetBool("BobDown", false);
    }
}
