using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{

    [SerializeField]
    private int maxHp;
    private int attack;

    private int hp;

    private Vector3 lastSafePos;

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
    void Start()
    {
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddAttacked(int damage)
    {
        Hp -= damage;

        if (Hp <= 0)
        {
            Transform UI = GameObject.FindGameObjectWithTag("UI").transform;
            for (int i = 0; i < UI.childCount; i++)
            {
                if (UI.GetChild(i).name == "GameClear")
                {
                    StartCoroutine(UI.GetChild(i).GetComponent<GameClear>().ClearGame(false, 0, 0));
                    return;
                }
            }
        }
    }

    public void SetHp(int i)
    {
        hp = i;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Player collided with " + col.name);
        if (col.gameObject.name == "ReviveTile")
        {
            Debug.Log("Asdasd");
            lastSafePos = col.transform.position;
        }
    }

    public void MoveToLastSafePlace()
    {
        transform.position = lastSafePos;
    }
}
