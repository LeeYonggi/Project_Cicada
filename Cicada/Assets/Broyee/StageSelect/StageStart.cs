using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class StageStart : MonoBehaviour {

    public int map;
    public int stage;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartStage()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);

        string levelName = map.ToString() + "_" + stage.ToString();

        var mapData = MapData.Load("Assets/Resource/Xml/" + levelName + ".xml");

        GameObject.FindGameObjectWithTag("Player").gameObject.transform.position = mapData.playerPos;
        Instantiate(Resources.Load("prefab/LevelDesigns/" + levelName + "/" + levelName + "Map.prefab") as GameObject);
        for (int i = 0; i < mapData.monsters.Length; i++)
        {
            Instantiate(Resources.Load("prefab/Monsters/" + mapData.monsters[i].name + ".prefab") as GameObject).transform.position = mapData.monsters[i].pos;
        }
    }
}
