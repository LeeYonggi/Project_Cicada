using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    public static StageManager stageManager;

    public Transform stages;

    private int map;
    private int stage;

    public int stageStars;

    private void Awake()
    {
        if (stageManager == null)
        {
            DontDestroyOnLoad(gameObject);
            stageManager = this;
        }
        else
        {
            if (stageManager != this)
            {
                Destroy(gameObject);
            }
        }

        PlayerPrefs.SetInt("StageStars", stageStars);
    }

    // Use this for initialization
    void Start () {
        map = 1;
        stage = 1;

        for (int i = 0; i < stages.childCount - 2; i++)
        {
            stages.GetChild(i).GetChild(0).GetComponent<Stars>().enabledStarNum = PlayerPrefs.GetInt("StageStars") / (10 ^ stages.childCount- 2 - i);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int GetMap()
    {
        return map;
    }

    public int GetStage()
    {
        return stage;
    }

    public void SetMap(int _map)
    {
        map = _map;
    }

    public void SetStage(int _stage)
    {
        stage = _stage;
    }

    public void SetMapAndStage(int _map, int _stage)
    {
        map = _map;
        stage = _stage;

        Debug.Log("Map : " + map.ToString() + "  " + "Stage : " + stage.ToString());
    }
}
