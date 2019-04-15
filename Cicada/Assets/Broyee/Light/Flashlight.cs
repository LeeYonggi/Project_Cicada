using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{    
    [SerializeField] private float basicViewRange;
    [SerializeField] private float extendedViewRange;

    private bool turnedOn;
    private bool coolDowning;
    private bool decreasingBrightness;

    [SerializeField] private float duration;
    [SerializeField] private float coolDownTime;

    // Colors of flashlight button according to its state
    [SerializeField] private Color turnedOffColor;
    [SerializeField] private Color turnedOnColor;
    [SerializeField] private Color coolDownColor;

    [SerializeField] private float brightnessDecreasingSpeed;

    private Light spotLight;

    public Image flashLightButtonImage;

    // Use this for initialization
    void Start()
    {
        turnedOn = false;
        coolDowning = false;
        decreasingBrightness = false;

        spotLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (decreasingBrightness)
        {
            // Decrease brightness of flashlight
            spotLight.spotAngle -= brightnessDecreasingSpeed;
        }
    }

    public void TurnOn()
    {
        if (coolDowning) return;

        flashLightButtonImage.color = turnedOnColor;

        // Increase brightness of flashlight to its max
        spotLight.spotAngle = extendedViewRange;

        turnedOn = true;
        decreasingBrightness = true;

        StartCoroutine(TurnOffAfter());
    }

    private IEnumerator TurnOffAfter()
    {
        yield return new WaitForSeconds(duration);

        flashLightButtonImage.color = coolDownColor;

        // Decrease brightness of flashlight to its min
        spotLight.spotAngle = basicViewRange;

        turnedOn = false;
        coolDowning = true;
        decreasingBrightness = false;
        
        StartCoroutine(CoolDown());
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(coolDownTime);

        flashLightButtonImage.color = turnedOffColor;

        coolDowning = false;
    }
}