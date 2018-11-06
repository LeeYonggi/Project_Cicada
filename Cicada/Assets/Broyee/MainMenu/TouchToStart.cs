using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchToStart : MonoBehaviour {

    private UnityEngine.UI.Image image;

	// Use this for initialization
	void Start () {
        image = GetComponent<UnityEngine.UI.Image>();

        StartCoroutine(Blink());
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0))
        {
            Time.timeScale = 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
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
