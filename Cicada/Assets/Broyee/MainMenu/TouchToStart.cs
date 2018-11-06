using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchToStart : MonoBehaviour {

    private UnityEngine.UI.Image image;

	// Use this for initialization
	void Start () {
        image = GetComponent<UnityEngine.UI.Image>();


	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator Blink()
    {
        while (true)
        {
            //image.color = Color.
        }
    }
}
