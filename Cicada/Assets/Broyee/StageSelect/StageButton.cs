using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageButton : MonoBehaviour {

    public int map;
    public int stage;

    [HideInInspector] public bool playerIsOn;
    [HideInInspector] public bool locked;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()
    {
        if (!locked)
        {
            GameObject stageGenerator = GameObject.FindObjectOfType(typeof(StageGenerator)) as GameObject;
            stageGenerator.GetComponent<StageGenerator>().SetMapAndStage(map, stage);

            float playerYPos = transform.parent.Find("Player").transform.position.y;
            if (transform.position.y > playerYPos)
                StartCoroutine(transform.parent.Find("Player").GetComponent<PlayerInStageSelect>().Jump(transform.position.y));
            else
                StartCoroutine(transform.parent.Find("Player").GetComponent<PlayerInStageSelect>().Fall(transform.position.y));
        }
    }
    

}
