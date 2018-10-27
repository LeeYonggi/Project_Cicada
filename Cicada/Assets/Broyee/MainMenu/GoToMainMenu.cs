using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMainMenu : MonoBehaviour {

    public void OnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
