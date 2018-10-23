using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInfo : MonoBehaviour {

    private bool falling;
    private float prevHeight;

    public int fallingDamage;

    private void Start()
    {
        falling = true;
    }


    // Update is called once per frame
    void Update () {

        if (transform.position.y == prevHeight)
        {
            falling = false;
        }
        else
        {
            //Debug.Log("Falling");
            falling = true;
        }

        prevHeight = transform.position.y;
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("Block collided");
        if (falling)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                //col.gameObject.GetComponent<PlayerTrap>().Hurt(fallingDamage);
            }
            if (col.gameObject.CompareTag("Monster") || col.gameObject.CompareTag("BossMonster"))
            {
                col.gameObject.GetComponent<MonsterInfo>().hp -= fallingDamage;
                
                
                if (col.gameObject.CompareTag("BossMonster"))
                {
                    Destroy(gameObject);
                }
                else
                    Destroy(col.gameObject);

            }
        }
        if (col.gameObject.name == "Ground" || col.gameObject.CompareTag("BossMonster"))
        {
            Destroy(gameObject);
        }
    }
}
