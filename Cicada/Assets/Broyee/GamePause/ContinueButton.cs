using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour {

    public void Continue()
    {
        GetComponent<AudioSource>().Play();

        Time.timeScale = 1;

        transform.parent.gameObject.SetActive(false);
    }
}
