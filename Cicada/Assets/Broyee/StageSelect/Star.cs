using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour {

    public Sprite emptyStar;
    public Sprite star;

    public bool starOn;
    
    private Image image;
    

	// Use this for initialization
	void Start () {

        image = GetComponent<Image>();

    }
	
	// Update is called once per frame
	void Update () {

        if (starOn) image.sprite = star;
        else image.sprite = emptyStar;
    }
}
