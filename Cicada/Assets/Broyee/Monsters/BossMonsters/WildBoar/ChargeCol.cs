using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeCol : MonoBehaviour {

    public float chargePushPower;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hitted by charge");

            Vector2 forceVec = new Vector2(transform.parent.GetComponent<HorizontalMonsterMove>().direction, 0);
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(forceVec * Time.deltaTime * chargePushPower, ForceMode2D.Impulse);
            //col.gameObject.GetComponent<PlayerTrap>().Hurt(transform.parent.GetComponent<WildBoarAttack>().chargeDamage);

            gameObject.SetActive(false);
        }
    }
    
}
