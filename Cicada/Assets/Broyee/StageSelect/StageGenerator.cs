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

        //var mapData = MapData.Load("Assets/Resource/Xml/" + levelName + ".xml");

        //Debug.Log(Application.dataPath);
        //Instantiate(Resources.Load("Prefabs/LevelDesigns/" + levelName + "/" + levelName + "Map"));
        //for (int i = 0; i < mapData.monsters.Length; i++)
        //{
        //    Debug.Log(mapData.monsters[i].name);
        //    Instantiate(Resources.Load("Prefabs/Monsters/" + mapData.monsters[i].name) as GameObject, mapData.monsters[i].pos, Quaternion.identity);
        //}
    }

    public void GenerateNextStage()
    {
        StageManager stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        int map = stageManager.GetMap();
        int stage = stageManager.GetStage() + 1;

        if (stage > 5)
        {
            map++;
            stage = 1;
        }

        string levelName = "Stage" + map.ToString() + "_" + stage.ToString();

        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }

    
}
