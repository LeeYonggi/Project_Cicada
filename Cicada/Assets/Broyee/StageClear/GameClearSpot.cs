using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearSpot : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine(GameObject.FindGameObjectWithTag("UI").transform.GetChild(6).GetComponent<GameClear>().ClearGame(true, 2, 567, 1));
        }
    }
}
