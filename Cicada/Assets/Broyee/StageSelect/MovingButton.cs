using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MovingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public float horizontalMoveDis;
    //public float verticalMoveDis;

    public float horizontalMoveSpeed;
    //public float verticalMoveSpeed;
    
    private int dir;
    private float basicXPos;
    private bool fullyMoved;
    private bool mouseIsOver;

    // Use this for initialization
    void Start()
    {
        basicXPos = transform.position.x;

        if (horizontalMoveSpeed > 0) dir = 1;
        else dir = -1;

        fullyMoved = false;
        
    }

    private void Update()
    {
        if (mouseIsOver)
        {
            if (fullyMoved) return;

            //Debug.Log("ButtonMove");
            transform.Translate(horizontalMoveSpeed, 0, 0);

            if (transform.position.x * dir > (basicXPos + horizontalMoveDis) * dir)
            {
                transform.position = new Vector3(basicXPos + horizontalMoveDis, transform.position.y, transform.position.z);

                fullyMoved = true;
            }
        }
        if (!mouseIsOver)
        {
            if (transform.position.x  < basicXPos)
            {
                transform.Translate(horizontalMoveSpeed * -1, 0, 0);
            }
            else if (transform.position.x != basicXPos)
                transform.position = new Vector3(basicXPos, transform.position.y, transform.position.z);

        }
        
        if (transform.position.x != basicXPos + horizontalMoveDis) fullyMoved = false;
    }


    private void OnMouseOver()
    {
        if (fullyMoved) return;

        Debug.Log("ButtonMove");
        transform.Translate(horizontalMoveSpeed, 0, 0);

        if (transform.position.x * dir > basicXPos + horizontalMoveDis)
        {
            transform.Translate((basicXPos + horizontalMoveSpeed - transform.position.x) * dir, 0, 0);

            fullyMoved = true;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointerEnter");
        mouseIsOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //do stuff
        mouseIsOver = false;
    }


}
