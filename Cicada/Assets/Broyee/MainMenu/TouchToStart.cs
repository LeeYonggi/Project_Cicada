using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchToStart : MonoBehaviour {

    [HideInInspector] bool readyToStart;
    private Image image;

    private AudioSource[] audioSources;

    public Image screen;
    public Image logos;

    private bool started;
    
    // Use this for initialization
    void Start () {
        image = GetComponent<Image>();
        audioSources = GetComponents<AudioSource>();

        readyToStart = true;
        started = false;

        //StartCoroutine(ScreenStart());
        audioSources[0].Play();
        StartCoroutine(Blink());
    }
	
	// Update is called once per frame
	void Update () {

        if (readyToStart && !started)
        {
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0))
            {
                started = true;
                StartCoroutine(StartGame());
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

        audioSources[0].Play();
        StartCoroutine(Blink());
    }

    private IEnumerator StartGame()
    {
        StopCoroutine(Blink());
        image.color = new Color(1, 1, 1, 1);

        audioSources[0].Stop();
        audioSources[1].Play();

        yield return new WaitForSeconds(1.0f);

        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    private IEnumerator Blink()
    {
        if (!started)
        {
            image.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(0.3f);

            image.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.3f);

            StartCoroutine(Blink());
        }
    }
}
