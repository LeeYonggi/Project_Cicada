using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour {

    public Sprite emptyImage;
    public Sprite image;

    private bool isDamaged;

    public bool IsDamaged
    {
        get
        {
            return isDamaged;
        }

        set
        {
            isDamaged = value;
        }
    }

    // Use this for initialization
    void Start () {
        IsDamaged = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(IsDamaged)
        {
            GetComponent<Image>().sprite = emptyImage;
        }
        else
        {
            GetComponent<Image>().sprite = image;
        }

    }
}
