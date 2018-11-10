using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public List<GameObject> cameraZone = new List<GameObject>();
    public GameObject player;
    public float range;
    public Vector2 cameraSize;

    private BoxCollider2D cameraBox;
    private int nowCameraBox;
    private int pastCameraBox;
    public bool isMoveX;
    public bool isMoveY;
    public bool isStop;
    private bool isShake;

    private bool isChangeCameraBox;

    // Use this for initialization
    void Start () {
        nowCameraBox = 0;
        pastCameraBox = 0;
        cameraBox = cameraZone[nowCameraBox].GetComponent<BoxCollider2D>();
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        isShake = false;
        isChangeCameraBox = false;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 tempPos;
        //if (isShake == true) return;

        //if (player.transform.position.y - transform.position.y > 0)
        if (isChangeCameraBox)
        {
            Vector3 targetPos = Vector3.zero;
            if(pastCameraBox < nowCameraBox)
                targetPos = (Vector2)cameraZone[nowCameraBox].transform.position - cameraBox.size * 0.5f + cameraBox.offset + cameraSize * 0.5f;
            if (pastCameraBox > nowCameraBox)
                targetPos = (Vector2)cameraZone[nowCameraBox].transform.position + new Vector2(cameraBox.size.x * 0.5f, -cameraBox.size.y * 0.5f) + 
                    cameraBox.offset - new Vector2(cameraSize.x * 0.5f, -cameraSize.y * 0.5f);

            if(targetPos.x - 0.1f < transform.position.x && pastCameraBox < nowCameraBox)
            {
                isChangeCameraBox = false;
            }
            else if(targetPos.x + 0.1f > transform.position.x && pastCameraBox > nowCameraBox)
            {
                isChangeCameraBox = false;
            }
            tempPos.x = (targetPos.x - transform.position.x) / (range * 1.5f);
            tempPos.y = (player.transform.position.y - transform.position.y) / (range * 1.5f);
            if (isMoveX == false) tempPos.x = 0;
            if (isMoveY == false) tempPos.y = 0;
            tempPos.z = 0;
            transform.Translate(tempPos);
            WorkCameraZoneY();
        }    
        else
        {
            tempPos = (player.transform.position - transform.position) / range;
            if (isMoveX == false) tempPos.x = 0;
            if (isMoveY == false) tempPos.y = 0;
            tempPos.z = 0;
            transform.Translate(tempPos);
        }
        //else
        //{
        //    tempPos = (player.transform.position - transform.position) / range;
        //    transform.Translate(new Vector3(tempPos.x, 0, 0));
        //}

        float tempX  = cameraBox.size.x * 0.5f + cameraZone[nowCameraBox].transform.position.x + cameraBox.offset.x;
        float ltempX = -cameraBox.size.x * 0.5f + cameraZone[nowCameraBox].transform.position.x + cameraBox.offset.x;
        if (player.transform.position.x > tempX && nowCameraBox < cameraZone.Count - 1)
        {
            pastCameraBox = nowCameraBox;
            ++nowCameraBox;
            cameraBox = cameraZone[nowCameraBox].GetComponent<BoxCollider2D>();
            isChangeCameraBox = true;
        }
        else if(player.transform.position.x < ltempX && nowCameraBox > 0)
        {
            pastCameraBox = nowCameraBox;
            --nowCameraBox;
            cameraBox = cameraZone[nowCameraBox].GetComponent<BoxCollider2D>();
            isChangeCameraBox = true;
        }

        if(isChangeCameraBox)
        {
            
        }
        if (isChangeCameraBox == false)
        {
            WorkCameraZoneY();
            WorkCameraZoneX();
        }
    }

    void WorkCameraZoneX()
    {
        float cameraLineX = cameraBox.size.x / 2.0f;
        float cameraCompareX = cameraZone[nowCameraBox].transform.position.x + cameraLineX + cameraBox.offset.x 
            - (cameraSize.x / 2.0f + transform.position.x);
        float lCameraCompareX = cameraZone[nowCameraBox].transform.position.x + -cameraLineX + cameraBox.offset.x 
            - (-cameraSize.x / 2.0f + transform.position.x);

        if (cameraCompareX < 0.0f)
            transform.Translate(new Vector3(cameraCompareX, 0, 0));
        else if (lCameraCompareX > 0.0f)
            transform.Translate(new Vector3(lCameraCompareX, 0, 0));
    }
    void WorkCameraZoneY()
    {
        float cameraLineY = cameraBox.size.y / 2.0f;

        float cameraCompareY = cameraZone[nowCameraBox].transform.position.y + 
            cameraLineY + cameraBox.offset.y - (cameraSize.y / 2.0f + transform.position.y);
        float lCameraCompareY = cameraZone[nowCameraBox].transform.position.y + 
            -cameraLineY + cameraBox.offset.y - (-cameraSize.y / 2.0f + transform.position.y);

        if (cameraCompareY < 0.0f)
            transform.Translate(new Vector3(0, cameraCompareY, 0));
        else if (lCameraCompareY > 0.0f)
            transform.Translate(new Vector3(0, lCameraCompareY, 0));
    }


    public void ShakeCamera()
    {
        StartCoroutine(shakeCoroutine());
    }

    IEnumerator shakeCoroutine()
    {
        isShake = true;
        for (int i = 0; i < 10; i++)
        {
            transform.Translate(new Vector2(Random.Range(-5, 5) * 0.01f, Random.Range(-5, 5) * 0.01f));
            yield return new WaitForSeconds(0.03f);
        }
        isShake = false;
    }
}
