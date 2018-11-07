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

        for (int i = 0; i < transform.parent.childCount - 1; i++)
        {
            transform.parent.GetChild(i).gameObject.SetActive(false);
        }

        transform.GetChild(0).gameObject.SetActive(true);

        if (clear)
            transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
        else
            transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);



        transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<Text>().text = "Score : " + score.ToString();


    }


}
