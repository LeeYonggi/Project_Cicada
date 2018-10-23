using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthWormRadar : MonoBehaviour {

    [HideInInspector] public bool playerIsInView;

	// Use this for initialization
	void Start () {
        playerIsInView = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerIsInView = true;
        }
    }
}
