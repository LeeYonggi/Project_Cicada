using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingBlock : MonoBehaviour {

    private Rigidbody2D rb;

    [HideInInspector] public Vector3 playerPos;
    public float speed;

	// Use this for initialization
	void Start () {

        GetComponent<Rigidbody2D>().AddForce((playerPos - transform.position).normalized * Time.deltaTime * speed, ForceMode2D.Impulse);
        //GetComponent<Rigidbody2D>().AddForce(transform.up * Time.deltaTime * speed, ForceMode2D.Impulse);
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            //col.GetComponent<PlayerTrap>().Hurt(GetComponent<BlockInfo>().fallingDamage);
            Destroy(gameObject);
        }
    }
}
