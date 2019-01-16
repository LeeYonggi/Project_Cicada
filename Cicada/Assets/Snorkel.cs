using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snorkel : MonoBehaviour {

    public Sprite frontSprite;
    public Sprite sideSprite;

    public Vector3 initfrontPos;
    public Vector3 initSidePos;

    public GameObject parent;

    private SpriteRenderer spRenderer;
    // Use this for initialization
    void Start () {
        spRenderer = GetComponent<SpriteRenderer>();
        //transform.position = initfrontPos + parent.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 distance = initfrontPos;

        float velocity = parent.GetComponent<Animator>().GetFloat("velocityX");
        if (velocity > 0.001)
        {
            distance = initSidePos;
            spRenderer.sprite = sideSprite;
        }
        else
            spRenderer.sprite = frontSprite;
        transform.position = parent.transform.position;
		if(parent.GetComponent<SpriteRenderer>().flipX == true)
        {
            distance.x = -distance.x;
            transform.Translate(distance);
            spRenderer.flipX = true;
        }
        else
        {
            transform.Translate(distance);
            spRenderer.flipX = false;
        }

	}
}
