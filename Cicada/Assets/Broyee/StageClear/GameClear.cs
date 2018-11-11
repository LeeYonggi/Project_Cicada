using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClear : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public IEnumerator ClearGame(bool clear, int star, int score, int stage)
    {
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 0;

        // Disable other UIs
        for (int i = 0; i < transform.parent.childCount - 2; i++)
        {
            transform.parent.GetChild(i).gameObject.SetActive(false);
        }

        // Enable Whole UIs
        transform.GetChild(0).gameObject.SetActive(true);

        // Clear screen or fail screen
        if (clear)
            transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        else
            transform.GetChild(0).GetChild(1).gameObject.SetActive(true);

        // Stars
        //for (int i = 0; i < star; i++)
        //{
        //    transform.GetChild(0).GetChild(2).GetChild(i + 3).gameObject.SetActive(true);
        //}


        //transform.GetChild(0).GetChild(3).GetComponent<Text>().text = "Score : " + score.ToString();


    }


}
