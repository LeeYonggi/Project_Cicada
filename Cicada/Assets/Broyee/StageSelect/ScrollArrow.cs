using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollArrow : MonoBehaviour {

    public bool isOnUp;

    public Sprite brightSprite;
    public Sprite darkSprite;

    private UnityEngine.UI.Image image;

    private void Start()
    {
        image = GetComponent<UnityEngine.UI.Image>();
    }

    // Update is called once per frame
    void Update () {

        if (isOnUp)
        {            
            if (transform.parent.GetChild(1).GetChild(0).position.y < -200)
            {
                image.sprite = darkSprite;
            }
            else
                image.sprite = brightSprite;
        }
        else
        {
            if (transform.parent.GetChild(1).GetChild(0).position.y > 1630)
            {
                image.sprite = darkSprite;
            }
            else
                image.sprite = brightSprite;
        }
	}
}
