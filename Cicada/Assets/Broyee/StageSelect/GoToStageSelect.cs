using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GoToStageSelect : MonoBehaviour {

    public void OnClick()
    {
        GetComponent<AudioSource>().Play();

        Time.timeScale = 1;
        //UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
