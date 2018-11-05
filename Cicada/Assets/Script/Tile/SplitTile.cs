using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitTile : MonoBehaviour {

    enum SPLIT_STATE
    {
        LITTLE,
        MUCH,
        DESTROY
    }

    public Sprite muchSprite;

    private SPLIT_STATE m_eSplitState;
    [SerializeField]
    private float splitDelay = 3.0f;

    private SpriteRenderer m_SpRenderer;
    private bool isPlayerOn;

    public GameObject playerPhysics;

    // Use this for initialization
    void Start () {
        m_eSplitState = SPLIT_STATE.LITTLE;

        m_SpRenderer = GetComponent<SpriteRenderer>();
        isPlayerOn = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (playerPhysics.GetComponent<PhysicsObject>().Grounded == false)
            isPlayerOn = false;
        if (isPlayerOn)
            splitDelay -= Time.deltaTime;
        if (splitDelay < 1.5f)
        {
            m_eSplitState = SPLIT_STATE.MUCH;
            m_SpRenderer.sprite = muchSprite;
        }
	}
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player_Bottom")
        {
            isPlayerOn = true;
        }
    }
}
