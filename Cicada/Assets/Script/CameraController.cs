using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject cameraZone;
    public GameObject player;
    public float range;
    public Vector2 cameraSize;

    private BoxCollider2D cameraBox;

    // Use this for initialization
    void Start () {
        cameraBox = cameraZone.GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 tempPos = (player.transform.position - transform.position) / range;
        transform.Translate(new Vector3(tempPos.x, tempPos.y, 0));
        WorkCameraZone();
    }

    void WorkCameraZone()
    {
        float cameraLineX = cameraBox.size.x / 2.0f;
        float cameraLineY = cameraBox.size.y / 2.0f;

        float cameraCompareX = cameraLineX + cameraBox.offset.x - (cameraSize.x / 2.0f + transform.position.x);
        float lCameraCompareX = -cameraLineX + cameraBox.offset.x - (-cameraSize.x / 2.0f + transform.position.x);

        float cameraCompareY = cameraLineY + cameraBox.offset.y - (cameraSize.y / 2.0f + transform.position.y);
        float lCameraCompareY = -cameraLineY + cameraBox.offset.y - (-cameraSize.y / 2.0f + transform.position.y);

        if (cameraCompareX < 0.0f)
            transform.Translate(new Vector3(cameraCompareX, 0, 0));
        else if (lCameraCompareX > 0.0f)
            transform.Translate(new Vector3(lCameraCompareX, 0, 0));
        if (cameraCompareY < 0.0f)
            transform.Translate(new Vector3(0, cameraCompareY, 0));
        if (lCameraCompareY > 0.0f)
            transform.Translate(new Vector3(0, lCameraCompareY, 0));
    }
}
