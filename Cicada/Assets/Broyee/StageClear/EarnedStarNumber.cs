using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarnedStarNumber : MonoBehaviour {

    public Transform stages;

    public int earnedStarNum;

    private void OnLevelWasLoaded(int level)
    {
        stages = GameObject.Find("Stages").transform;
    }

    // Use this for initialization
    void Start () {

        earnedStarNum = 0;

        for (int i = 0; i < stages.childCount - 3; i++)
        {
            if (stages.GetChild(i).GetChild(0).GetComponent<Stars>().enabledStarNum != 4)
            earnedStarNum += stages.GetChild(i).GetChild(0).GetComponent<Stars>().enabledStarNum;

            //Debug.Log(earnedStarNum);
        }

        GetComponent<Text>().text = earnedStarNum.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
