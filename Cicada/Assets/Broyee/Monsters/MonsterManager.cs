using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour {

    public float respawnTime;

    private bool[] respawning;

	// Use this for initialization
	void Start () {
        respawning = new bool[transform.childCount];
	}
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).gameObject.activeSelf && !respawning[i])
            {
                StartCoroutine(Respawn(i));
                respawning[i] = true;
            }
        }
	}

    private IEnumerator Respawn(int index)
    {

        yield return new WaitForSeconds(respawnTime);

        transform.GetChild(index).gameObject.SetActive(true);
        transform.GetChild(index).GetComponent<MonsterInfo>().dead = false;

        respawning[index] = false;
    }
}
