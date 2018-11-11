using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : MonoBehaviour {

    enum DIRECTION
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    public int direction = 0;
    public float leftRange;
    public float rightRange;
    public float moveSpeed;
    public bool isCircle;
    public float radius;
    public GameObject player;
    public float runningTime;


    private Vector2 moveVec2;
    private BoxCollider2D m_BoxCollider;
    private Vector2 initPosition;

    // Use this for initialization
    void Start () {
        moveVec2 = new Vector2(0, 0);
        m_BoxCollider = GetComponent<BoxCollider2D>();
        initPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (isCircle == false)
        {
            switch (direction)
            {
                case (int)DIRECTION.LEFT:
                    transform.Translate(new Vector2(-moveSpeed * Time.deltaTime, 0));
                    moveVec2.x += -moveSpeed * Time.deltaTime;

                    if (moveVec2.x < leftRange)
                        direction = (int)DIRECTION.RIGHT;
                    break;
                case (int)DIRECTION.RIGHT:
                    transform.Translate(new Vector2(moveSpeed * Time.deltaTime, 0));
                    moveVec2.x += moveSpeed * Time.deltaTime;

                    if (moveVec2.x > rightRange)
                        direction = (int)DIRECTION.LEFT;
                    break;
                case (int)DIRECTION.UP:
                    transform.Translate(new Vector2(0, moveSpeed * Time.deltaTime));
                    moveVec2.y += moveSpeed * Time.deltaTime;

                    if (moveVec2.y > rightRange)
                        direction = (int)DIRECTION.DOWN;
                    break;
                case (int)DIRECTION.DOWN:
                    transform.Translate(new Vector2(0, -moveSpeed * Time.deltaTime));
                    moveVec2.y += -moveSpeed * Time.deltaTime;

                    if (moveVec2.y < leftRange)
                        direction = (int)DIRECTION.UP;
                    break;
                default:
                    break;
            }
            if (m_BoxCollider.isTrigger == false)
            {
                switch (direction)
                {
                    case (int)DIRECTION.LEFT:
                        player.transform.Translate(new Vector2(-moveSpeed * Time.deltaTime, 0));
                        break;
                    case (int)DIRECTION.RIGHT:
                        player.transform.Translate(new Vector2(moveSpeed * Time.deltaTime, 0));
                        break;
                    case (int)DIRECTION.UP:
                        player.transform.Translate(new Vector2(0, moveSpeed * Time.deltaTime));
                        break;
                    case (int)DIRECTION.DOWN:
                        player.transform.Translate(new Vector2(0, -moveSpeed * Time.deltaTime));
                        break;
                }
            }
        }
        else
        {
            float tempTime = runningTime;
            runningTime += Time.deltaTime * moveSpeed;
            float x = initPosition.x + radius * Mathf.Cos(runningTime);
            float y = initPosition.y + radius * Mathf.Sin(runningTime);
            this.transform.position = new Vector2(x, y);
            if (m_BoxCollider.isTrigger == false)
            {
                player.transform.Translate(new Vector2(radius * Mathf.Cos(runningTime) - radius * Mathf.Cos(tempTime), 
                    radius * Mathf.Sin(runningTime) - radius * Mathf.Sin(tempTime)));
            }
        }
    }
}
