using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour {

    private static InGameManager instance = null;

    public static InGameManager Instance
    {
        get
        {
            if (!instance)
                instance = FindObjectOfType(typeof(InGameManager)) as InGameManager;
            return instance;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
