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

        GameObject monsters = GameObject.FindGameObjectWithTag("Monsters");
        mapData.monsters = new MonsterData[monsters.transform.childCount];
        for (int i = 0; i < monsters.transform.childCount; i++)
        {
            Debug.Log("i : " + i.ToString());
            mapData.monsters[i].name = monsters.transform.GetChild(i).gameObject.name;
            mapData.monsters[i].pos = monsters.transform.GetChild(i).transform.position;
        }

        mapData.Save("Assets/Resource/Xml/" + stageName + ".xml");
    }
}
