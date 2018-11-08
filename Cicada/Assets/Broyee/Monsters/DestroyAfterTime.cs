using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {

    public float remainingDur;

	// Use this for initialization
	void Start () {
        StartCoroutine(DestroyAfter());
	}

    private IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(remainingDur);

        Destroy(gameObject);
    }
}
