using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundWhenEnabled : MonoBehaviour {

    private void OnEnable()
    {
        GetComponent<AudioSource>().Play();
    }
}
