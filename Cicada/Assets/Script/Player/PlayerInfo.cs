using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {

    [SerializeField]
    private int maxHp;
    private int attack;

    private int hp;

    #region GetSet
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

    public int MaxHp
    {
        get
        {
            return maxHp;
        }

        set
        {
            maxHp = value;
        }
    }

    public int Hp
    {
        get
        {
            return hp;
        }

        set
        {
            hp = value;
        }
    }
    #endregion
    // Use this for initialization
    void Start () {
        hp = maxHp;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddAttacked(int damage)
    {
        Hp -= damage;

        if (Hp <= 0)
        {
            StartCoroutine(GameObject.FindGameObjectWithTag("UI").transform.GetChild(5).GetComponent<GameClear>().ClearGame(false, 2, 567, 1));
        }
    }
}
