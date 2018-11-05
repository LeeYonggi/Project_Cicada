using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour {

    public int map;
    public int stage;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GenerateStage();
    }

    public void GenerateStage()
    {
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

    public void SetMapAndStage(int _map, int _stage)
    {
        map = _map;
        stage = _stage;

        Debug.Log("Map : " + map.ToString() + "  " + "Stage : " + stage.ToString());
    }
}
