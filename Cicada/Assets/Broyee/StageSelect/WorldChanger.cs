using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldChanger : MonoBehaviour {

    public List<Sprite> worldSprites;

    public List<float> dividingLines;

    public Transform stages;

    private Image image;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();

        //for (int i = 0; i < dividingLines.Capacity; i++)
        //{
        //    dividingLines[i] = Camera.main.WorldToScreenPoint(new Vector3(0, dividingLines[i], 0)).y;
        //}
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Stage y : " + stages.position.y);
        for (int i = 0; i < worldSprites.Capacity; i++)
        {            
            if (stages.position.y > dividingLines[i])
            {
                //Debug.Log("World " + (i + 1));

                image.sprite = worldSprites[i];
                break;
            }
        }

	}
}
