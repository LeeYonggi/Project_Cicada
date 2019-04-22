using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditButton : MonoBehaviour {



    public void EnableCreditScreen()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void Update()
    {
        if (transform.GetChild(0).gameObject.activeSelf && Input.anyKeyDown) transform.GetChild(0).gameObject.SetActive(false);
    }
}
