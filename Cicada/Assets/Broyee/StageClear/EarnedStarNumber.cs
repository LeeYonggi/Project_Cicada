using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarnedStarNumber : MonoBehaviour {

    public Transform stages;

    public int earnedStarNum;

	// Use this for initialization
	void Start () {

        for (int i = 0; i < stages.childCount - 2; i++)
        {
            earnedStarNum += stages.GetChild(i).GetChild(0).GetComponent<Stars>().enabledStarNum;
        }

        GetComponent<Text>().text = earnedStarNum.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
