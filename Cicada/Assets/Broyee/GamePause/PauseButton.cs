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
        if (transform.GetChild(0).gameObject.activeSelf)
        {
            Time.timeScale = 1;

            transform.GetChild(0).gameObject.SetActive(false);

            return;
        }

        Time.timeScale = 0;

        transform.GetChild(0).gameObject.SetActive(true);

    }
}
