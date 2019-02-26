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


    public IEnumerator ClearGame(bool clear, int star, int score)
    {
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 0;

        // Disable other UIs
        for (int i = 0; i < transform.parent.childCount - 2; i++)
        {
            transform.parent.GetChild(i).gameObject.SetActive(false);
        }
        transform.parent.GetChild(transform.parent.childCount - 1).gameObject.SetActive(false);

        // Enable Whole UIs
        transform.GetChild(0).gameObject.SetActive(true);

        // Clear screen or fail screen
        if (clear)
            transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        else
        {
            transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).GetChild(7).gameObject.SetActive(true);
        }
        
        // Stars
        for (int i = 0; i < star; i++)
        {
            transform.GetChild(0).GetChild(2).GetChild(i + 3).gameObject.SetActive(true);
        }

        StageManager stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        int currentStageOrder = (stageManager.GetMap() - 1) * 5 + stageManager.GetStage() - 1;

        int stageStars = PlayerPrefs.GetInt("StageStars");
        int[] dividedStageStars = new int[stageManager.stageNum];
        for (int i = 0; stageStars != 0; i++)
        {
            Debug.Log("i : " + i);
            if (currentStageOrder == i)
            {
                dividedStageStars[i] = star;
                Debug.Log("StarNum : " + star);
            }
            else dividedStageStars[i] = stageStars % 10;
            stageStars /= 10;
        }

        for (int i = 0; i < stageManager.stageNum; i++)
        {
            stageStars += dividedStageStars[i] * (int)(Mathf.Pow(10.0f, 1.0f * i));
        }

        PlayerPrefs.SetInt("StageStars", stageStars);
        Debug.Log("StageStars after game clear : " + stageStars);
        

        //transform.GetChild(0).GetChild(3).GetComponent<Text>().text = "Score : " + score.ToString();


    }

    public void Release()
    {
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        transform.GetChild(0).GetChild(7).gameObject.SetActive(false);

        transform.GetChild(0).gameObject.SetActive(false);
    }
}
