using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageButton : MonoBehaviour {

    public bool playerIsOn;

    public bool locked;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()
    {
        if (!locked)
        {
            float playerYPos = transform.parent.Find("Player").transform.position.y;
            if (transform.position.y > playerYPos)
                StartCoroutine(transform.parent.Find("Player").GetComponent<PlayerInStageSelect>().Jump(transform.position.y));
            else
                StartCoroutine(transform.parent.Find("Player").GetComponent<PlayerInStageSelect>().Fall(transform.position.y));
        }
    }
    

}
