using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Pause()
    {
        GetComponent<AudioSource>().Play();

        Time.timeScale = 0;

        transform.GetChild(0).gameObject.SetActive(true);
    }
}
