using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : MonoBehaviour {

    public int collisionDamage;

    public float moveSpeed;
    public float moveSpeedForAtk;

    // Use this for initialization
    private void Start()
    {
        StartCoroutine(SetAttackAfter(0.3f, true));
    }

    // Update is called once per frame
    void Update () {

        if (GetComponent<MonsterInfo>().attacking)
        {
            Debug.Log("SlimeAttack");

            Vector2 colStart = new Vector2();
            Vector2 colSize = new Vector2();

            GameObject col;
            if (Physics2D.OverlapBox(colStart, colSize, 0) != null)
            {
                col = Physics2D.OverlapBox(colStart, colSize, 0).gameObject;
                if (col.CompareTag("Player"))
                {
                    //col.GetComponent<PlayerTrap>().Hurt(collisionDamage);
                }
            }

            if (GetComponent<MonsterInfo>().GetPlayerRecognized())
            {
                GetComponent<HorizontalMonsterMove>().moveSpeed = moveSpeedForAtk;
            }
            else
            {
                GetComponent<HorizontalMonsterMove>().moveSpeed = moveSpeed;
            }
        }

	}

    IEnumerator SetAttackAfter(float time, bool _attaking)
    {
        yield return new WaitForSeconds(time);

        GetComponent<MonsterInfo>().attacking = _attaking;
    }

    
}
