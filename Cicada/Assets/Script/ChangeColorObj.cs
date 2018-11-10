using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorObj : MonoBehaviour {

    private SpriteRenderer m_SpRenderer;

    public float tempAlpha;
    public float minAlpha;
    public float maxAlpha;

    // Use this for initialization
    void Start () {
        m_SpRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (m_SpRenderer.color.a <= minAlpha || m_SpRenderer.color.a >= maxAlpha)
            tempAlpha = -tempAlpha;
        m_SpRenderer.color = new Color(m_SpRenderer.color.r, m_SpRenderer.color.g, m_SpRenderer.color.b, 
            m_SpRenderer.color.a + tempAlpha);

    }
}
