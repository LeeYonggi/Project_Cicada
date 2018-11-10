﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchToStart : MonoBehaviour {

    [HideInInspector] bool readyToStart;
    private Image image;

    public Image screen;
    public Image logos;
    
    // Use this for initialization
    void Start () {
        image = GetComponent<Image>();

        readyToStart = false;

        StartCoroutine(ScreenStart());
    }
	
	// Update is called once per frame
	void Update () {

        if (readyToStart)
        {
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0))
            {
                Time.timeScale = 1;
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            }
        }
	}

    private IEnumerator ScreenStart()
    {
        // Screen
        for (float i = 0; i < 1; i += 0.05f)
        {
            yield return new WaitForSeconds(0.1f);
            screen.color = new Color(i, i, i, 1);
        }

        // Logos
        for (float i = 0; i < 1; i += 0.05f)
        {
            yield return new WaitForSeconds(0.1f);
            logos.color = new Color(1, 1, 1, i);
        }

        yield return new WaitForSeconds(2.0f);

        screen.gameObject.SetActive(false);
        logos.gameObject.SetActive(false);

        readyToStart = true;

        StartCoroutine(Blink());
    }

    private IEnumerator StartGame()
    {
        GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(1.0f);

        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    private IEnumerator Blink()
    {
        image.color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(0.3f);

        image.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.3f);

        StartCoroutine(Blink());
    }
}
