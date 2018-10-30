using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMainGame : MonoBehaviour {

    public void OnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
