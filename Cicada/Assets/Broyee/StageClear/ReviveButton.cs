using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveButton : MonoBehaviour {

    public HpHeart hpManager;

    public void Revive()
    {
        transform.parent.parent.GetComponent<GameClear>().alreadyRevived = true;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerInfo>().SetHp(3);
        player.GetComponent<PlayerController>().BeInvincibleForSec(4.0f);
        player.GetComponent<PlayerController>().CallAttackedCoroutine(11);
        player.GetComponent<PlayerController>().StopMove();
        player.GetComponent<PlayerInfo>().MoveToLastSafePlace();


        Time.timeScale = 1;

        for (int i = 0; i < transform.parent.parent.parent.childCount; i++)
        {
            transform.parent.parent.parent.GetChild(i).gameObject.SetActive(true);
        }
        transform.parent.parent.parent.GetChild(2).gameObject.SetActive(false);
        if (FindObjectOfType<StageManager>().GetMap() != 2) transform.parent.parent.parent.Find("FlashlightButton").gameObject.SetActive(false);
        transform.parent.parent.GetComponent<GameClear>().Release();
    }
}
