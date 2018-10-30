using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private float destroyDelay;
    private GameObject player;

    public void AttackInit(float _destroyDelay, GameObject _player)
    {
        destroyDelay = _destroyDelay;
        player = _player;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (destroyDelay < 0)
            Destroy(gameObject);
        else
            destroyDelay -= Time.deltaTime;

        if(player.GetComponent<SpriteRenderer>().flipX == true)
        {
            transform.position = new Vector3(player.transform.position.x - 0.7f, player.transform.position.y);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x + 0.7f, player.transform.position.y);
        }
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
<<<<<<< HEAD
        if (col.CompareTag("Monster"))
        {
            Destroy(col.gameObject);
=======
        if(collision.gameObject.tag == "Monster")
        {
            
>>>>>>> 067e372aa101c29d3eef381c6beff9429e7c4ba1
        }
    }
}
