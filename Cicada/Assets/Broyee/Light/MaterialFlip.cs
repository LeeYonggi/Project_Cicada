using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialFlip : MonoBehaviour {

    private SpriteRenderer spriteRenderer;

    public Material leftMat;
    public Material rightMat;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (spriteRenderer.flipX) spriteRenderer.material = leftMat;
        else spriteRenderer.material = rightMat;
    }
}
