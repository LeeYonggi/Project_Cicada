using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    public static StageManager stageManager;

    public Transform stages;

    private int map;
    private int stage;

    public int stageStars;

    public int stageNum;

    private void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            stages = GameObject.Find("Stages").transform;

            SetStageStars();
        }
    }

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

        
    }

    // Use this for initialization
    void Start () {
        map = 1;
        stage = 1;

        PlayerPrefs.SetInt("StageStars", stageStars);

        SetStageStars();
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

    private void SetStageStars()
    {
        int temp = PlayerPrefs.GetInt("StageStars");
        Debug.Log("StageStars : " + temp);

        for (int i = 0; i < stages.childCount - 3; i++)
        {
            //Debug.Log("i : " + i + ", EnabledStarNum : " + temp % 10);//PlayerPrefs.GetInt("StageStars") / (10 ^ stages.childCount - 2 - i));
            stages.GetChild(i).GetChild(0).GetComponent<Stars>().enabledStarNum = temp % 10;

            temp /= 10;
        }
    }

}

