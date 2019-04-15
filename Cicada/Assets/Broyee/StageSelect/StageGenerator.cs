using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour {
    
    public void GenerateStage()
    {
        StageManager stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        int map = stageManager.GetMap();
        int stage = stageManager.GetStage();

        string levelName = "Stage" + map.ToString() + "_" + stage.ToString();

        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }

    public void GenerateNextStage()
    {
        StageManager stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        int map = stageManager.GetMap();
        int stage = stageManager.GetStage();

        int stageNum = (map - 1) * 5 + stage;

        stage++;

        if (stage > 5)
        {
            map++;
            stage = 1;
        }

        stageManager.SetMap(map);
        stageManager.SetStage(stage);

        string levelName = "Stage" + map.ToString() + "_" + stage.ToString();

        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }

    
}
