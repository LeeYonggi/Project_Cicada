using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterView : MonoBehaviour {

    [HideInInspector] public Vector3 playerPos;

    public float localXPos;

    private bool seeingWall;

	// Use this for initialization
	void Start () {
        seeingWall = false;

        playerPos = new Vector3(0, 0, 0);
	}

    private void Update()
    {
        if (transform.parent.GetComponent<MonsterInfo>().dead) return;
    }

    // Update is called once per frame
    void LateUpdate () {

        transform.position = new Vector3(transform.parent.position.x + (localXPos * transform.parent.GetComponent<HorizontalMonsterMove>().direction), transform.position.y, 0);

        Vector3 tempRot = new Vector3(0, 0, 0);

        if (transform.position.x < 0)
            tempRot.z = 180;

        transform.Rotate(tempRot);
    }

    public int PlayerIsOnRight()
    {
        if (transform.position.x < playerPos.x)
            return 1;
        return -1;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
        {
            seeingWall = true;
        }
        else if (col.CompareTag("Player") && !seeingWall)
        {
            transform.parent.gameObject.GetComponent<MonsterInfo>().SeePlayer();
            playerPos = col.transform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
        {
            seeingWall = true;
        }
        if (col.CompareTag("Player"))
        {
            transform.parent.gameObject.GetComponent<MonsterInfo>().SeePlayer();
            playerPos = col.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
        {
            seeingWall = false;
        }
        else if (col.CompareTag("Player"))
        {
            transform.parent.gameObject.GetComponent<MonsterInfo>().MissPlayer();
        }
    }

}
