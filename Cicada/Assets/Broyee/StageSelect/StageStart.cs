using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        //GameObject.FindGameObjectWithTag("Player").gameObject.transform.position = mapData.playerPos;
        Debug.Log("Instantiate!!");
        Instantiate(Resources.Load("Prefabs/LevelDesigns/" + levelName + "/" + levelName + "Map") as GameObject, new Vector3(0, 0, 0), Quaternion.identity);
        for (int i = 0; i < mapData.monsters.Length; i++)
        {
            Debug.Log(mapData.monsters[i].name);
            Instantiate(Resources.Load("Prefabs/Monsters/" + mapData.monsters[i].name) as GameObject, mapData.monsters[i].pos, Quaternion.identity);
        }
    }
}
