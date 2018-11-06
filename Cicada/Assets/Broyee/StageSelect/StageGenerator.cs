using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour {

    //public static StageGenerator stageGenerator;

    //public static int map;
    //public static int stage;
    

    //private void Awake()
    //{
    //    if (stageGenerator == null)
    //    {
    //        DontDestroyOnLoad(gameObject);
    //        stageGenerator = this;
    //    }
    //    else
    //    {
    //        if (stageGenerator != this)
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
    //}

    private void Start()
    {
        GenerateStage();
    }

    public void GenerateStage()
    {
        StageManager stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        int map = stageManager.GetMap();
        int stage = stageManager.GetStage();

        string levelName = map.ToString() + "_" + stage.ToString();

        var mapData = MapData.Load("Assets/Resource/Xml/" + levelName + ".xml");

        //GameObject.FindGameObjectWithTag("Player").gameObject.transform.position = mapData.playerPos;
        Debug.Log("Instantiate!!");
        Instantiate(Resources.Load("Prefabs/LevelDesigns/" + levelName + "/" + levelName + "Map"));
        for (int i = 0; i < mapData.monsters.Length; i++)
        {
            Debug.Log(mapData.monsters[i].name);
            Instantiate(Resources.Load("Prefabs/Monsters/" + mapData.monsters[i].name) as GameObject, mapData.monsters[i].pos, Quaternion.identity);
        }
    }

    
}
