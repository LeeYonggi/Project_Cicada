using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pea : MonoBehaviour {

    public int damage;
    public float speed;

    private bool popped;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed * Time.deltaTime, ForceMode2D.Impulse);

        popped = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (popped && !GetComponent<AudioSource>().isPlaying)
        {
            Destroy(gameObject);
        }
	}

    public void Pop()
    {
        GetComponent<AudioSource>().Play();
        popped = true;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!popped)
        {
            if (col.CompareTag("Player") || col.CompareTag("Ground") || col.CompareTag("Wall"))
            {
                Pop();
            }
        }
    }
}
