using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour {

    public void Continue()
    {
        Time.timeScale = 1;

        transform.parent.parent.gameObject.SetActive(false);
    }
}
