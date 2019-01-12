using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour {

    public int enabledStarNum;

	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < 3; i++)
        {
            transform.GetChild(i).GetComponent<Star>().starOn = false;
        }

        for (int i = enabledStarNum - 1; i >= 0; i--)
        {
            transform.GetChild(i).GetComponent<Star>().starOn = true;
        }

        
    }
    
}
