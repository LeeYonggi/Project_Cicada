using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour {

    [SerializeField] private float basicViewRange;
    [SerializeField] private float extendedViewRange;

    private bool turnedOn;
    private bool coolDowning;

    [SerializeField] private float duration;
    [SerializeField] private float coolDownTime;

    [SerializeField] private Color turnedOffColor;
    [SerializeField] private Color turnedOnColor;
    [SerializeField] private Color coolDownColor;

    private Light spotLight;

    public Image flashLightButtonImage;

	// Use this for initialization
	void Start () {
        turnedOn = false;
        coolDowning = false;

        spotLight = GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update () {
        if (turnedOn)
        {
            spotLight.spotAngle = extendedViewRange;

        }
        else spotLight.spotAngle = basicViewRange;


    }

    public void TurnOn()
    {
        if (coolDowning) return;

        flashLightButtonImage.color = turnedOnColor;

        turnedOn = true;
        StartCoroutine(TurnOffAfter());
    }

    private IEnumerator TurnOffAfter()
    {
        yield return new WaitForSeconds(duration);

        flashLightButtonImage.color = coolDownColor;

        turnedOn = false;
        coolDowning = true;

        StartCoroutine(CoolDown());
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(coolDownTime);

        flashLightButtonImage.color = turnedOffColor;

        coolDowning = false;
    }
}
