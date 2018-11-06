using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {

    [SerializeField]
    private int hp;
    private int attack;

    public int Attack
    {
        get
        {
            return attack;
        }

        set
        {
            attack = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddAttacked(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            StartCoroutine(GameObject.FindGameObjectWithTag("UI").transform.GetChild(5).GetComponent<GameClear>().ClearGame(false, 2, 567, 1));
        }
    }
}
