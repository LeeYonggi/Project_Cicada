using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageButton : MonoBehaviour {

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
            MovePlayerToCurrentStage();
        }
    }

    void MovePlayerToCurrentStage()
    {
        transform.parent.Find("Player").position = transform.position;
    }

}
