using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class StageManager : MonoBehaviour {

    public static StageManager stageManager;

    public Transform stages;

    private int map;
    private int stage;

    public int stageStars;
    public int stageNum;

    private string textPath;

    private void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            stages = GameObject.Find("Stages").transform;

            PutStarsToStages();
        }
    }

    private void Awake()
    {
        if (stageManager == null)
        {
            DontDestroyOnLoad(gameObject);
            stageManager = this;

            if (!PlayerPrefs.HasKey("StageStars"))
            PlayerPrefs.SetInt("StageStars", stageStars);
        }
        else
        {
            if (stageManager != this)
            {
                Destroy(gameObject);
            }
        }        
    }
    
    void Start () {
        map = 1;
        stage = 1;

        PutStarsToStages();

        textPath = "Assets/resources/Stagestars.txt";
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
    }

    private void PutStarsToStages()
    {
        int temp = PlayerPrefs.GetInt("StageStars");

        for (int i = 0; i < stages.childCount - 3; i++)
        {
            stages.GetChild(i).GetChild(0).GetComponent<Stars>().enabledStarNum = temp % 10;
            temp /= 10;
        }
    }

    private void SetStageStars()
    {
        StreamWriter writer = new StreamWriter(textPath, true);
        writer.Write(stageStars.ToString());
        writer.Close();
    }

    private int GetStageStars()
    {
        StreamReader reader = new StreamReader(textPath);
        int temp = int.Parse(reader.ReadToEnd());
        reader.Close();

        return temp;
    }



}

