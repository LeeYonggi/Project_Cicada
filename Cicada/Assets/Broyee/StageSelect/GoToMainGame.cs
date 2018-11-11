using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMainGame : MonoBehaviour {

    public void OnClick()
    {
        Time.timeScale = 1;

        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
