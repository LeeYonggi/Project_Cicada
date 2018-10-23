using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCollision : MonoBehaviour {

    public int collisionDamage;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            //col.GetComponent<PlayerTrap>().Hurt(collisionDamage);
        }
    }
}
