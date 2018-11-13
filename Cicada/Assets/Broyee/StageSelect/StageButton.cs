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
            //GameObject stageGenerator = GameObject.FindObjectOfType(typeof(StageGenerator)) as GameObject;
            //stageGenerator.GetComponent<StageGenerator>().SetMapAndStage(map, stage);

            GameObject.Find("StageManager").GetComponent<StageManager>().SetMapAndStage(map, stage);

            transform.parent.Find("Player").GetComponent<PlayerInStageSelect>().Move(transform);
        }
    }
    

}
