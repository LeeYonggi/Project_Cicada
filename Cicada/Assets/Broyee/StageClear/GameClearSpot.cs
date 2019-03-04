using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearSpot : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Transform canvas = GameObject.FindGameObjectWithTag("UI").transform;
            
            StartCoroutine(canvas.Find("GameClear").GetComponent<GameClear>().ClearGame(true, col.GetComponent<PlayerInfo>().Hp, 0));
        }
    }
}
