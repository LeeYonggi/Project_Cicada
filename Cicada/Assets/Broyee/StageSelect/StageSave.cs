using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSave : MonoBehaviour {

    public string stageName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveStage();
        }
	}

    public void SaveStage()
    {
        MapData mapData = new MapData();

        mapData.playerPos = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;

        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        mapData.monsters = new MonsterData[monsters.Length];
        for (int i = 0; i < monsters.Length; i++)
        {
            mapData.monsters[i] = new MonsterData();
            mapData.monsters[i].name = monsters[i].name;
            mapData.monsters[i].pos = monsters[i].transform.position;
        }

        mapData.Save("Assets/Resource/Xml/" + stageName + ".xml");
    }
}
