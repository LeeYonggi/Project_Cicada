using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eel : MonoBehaviour {

    public Transform playerTrans;

    public float knockBackDis;

    private bool attacked;
    private bool appeared;

    private Vector3 basicPlayerPos;

	// Use this for initialization
	void Start () {

        attacked = false;
        appeared = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (!attacked && GetComponent<MonsterInfo>().GetPlayerIsInView())
        {
            StartCoroutine(Attack());
        }
	}
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Collided");
        if (appeared && col.CompareTag("Player"))
        {
            Debug.Log("Push player");
            int xDir = -1;
            int yDir = -1;
            if (playerTrans.position.x > transform.position.x) xDir = 1;
            if (playerTrans.position.y > transform.position.y) yDir = 1;

            basicPlayerPos = playerTrans.position;

            //col.GetComponent<Rigidbody2D>().AddForce(new Vector2(100.0f * dir, 1));
            StartCoroutine(KnockBack(xDir, yDir));

        }
    }

    IEnumerator Attack()
    {

        yield return new WaitForSeconds(0.5f);

        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        GetComponent<BoxCollider2D>().enabled = true;
        appeared = true;

        yield return new WaitForSeconds(2.0f);

        gameObject.SetActive(false);
    }

    IEnumerator KnockBack(int xDir, int yDir)
    {
        bool fullyMoved;

        while (true)
        {
            fullyMoved = true;

            if (playerTrans.position.x * xDir < (basicPlayerPos.x + (knockBackDis * xDir)) * xDir)
            {
                playerTrans.Translate(0.1f * xDir, 0, 0);
                yield return new WaitForEndOfFrame();

                fullyMoved = false;
            }

            if (playerTrans.position.y * yDir < (basicPlayerPos.y + (knockBackDis * yDir)) * yDir)
            {
                playerTrans.Translate(0, 0.1f * yDir, 0);
                yield return new WaitForEndOfFrame();

                fullyMoved = false;
            }

            if (fullyMoved)
            {
                Debug.Log("FullyMoved");
                yield return 0;
            }
        }

    }
}
