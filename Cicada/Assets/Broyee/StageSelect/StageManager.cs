using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    public static StageManager stageManager;

    private int map;
    private int stage;

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

    public void SetMapAndStage(int _map, int _stage)
    {
        map = _map;
        stage = _stage;

        Debug.Log("Map : " + map.ToString() + "  " + "Stage : " + stage.ToString());
    }
}
