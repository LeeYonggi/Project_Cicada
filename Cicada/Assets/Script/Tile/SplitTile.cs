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
    public Sprite littleSprite;

    private SPLIT_STATE m_eSplitState;
    [SerializeField]
    private float initDelay = 3.0f;
    [SerializeField]
    private float spawnInitDelay = 5.0f;

    private bool isDie;

    private float splitDelay;
    private float spawnDelay;

    private SpriteRenderer m_SpRenderer;
    private BoxCollider2D m_Bcollider2D;
    private bool isPlayerOn;

    public GameObject playerPhysics;

    // Use this for initialization
    void Start () {
        m_eSplitState = SPLIT_STATE.LITTLE;

        m_SpRenderer = GetComponent<SpriteRenderer>();
        littleSprite = m_SpRenderer.sprite;
        m_Bcollider2D = GetComponent<BoxCollider2D>();
        isPlayerOn = false;
        isDie = false;
        splitDelay = initDelay;
        spawnDelay = spawnInitDelay;
    }
	
	// Update is called once per frame
	void Update () {
        if (isDie == false)
        {
            //if (playerPhysics.GetComponent<PhysicsObject>().Grounded == false)
            //    isPlayerOn = false;
            if (isPlayerOn)
                splitDelay -= Time.deltaTime;
            if (splitDelay < initDelay / 2.0f)
            {
                m_eSplitState = SPLIT_STATE.MUCH;
                m_SpRenderer.sprite = muchSprite;
            }
            if (splitDelay < 0.0f)
            {
                m_Bcollider2D.enabled = false;
                m_SpRenderer.sprite = null;
                isDie = true;
            }
        }
        else
        {
            spawnDelay -= Time.deltaTime;
            if (spawnDelay < 0.0f)
            {
                isDie = false;
                splitDelay = initDelay;
                m_SpRenderer.sprite = littleSprite;
                m_Bcollider2D.enabled = true;
                spawnDelay = spawnInitDelay;
                isPlayerOn = false;
            }
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
