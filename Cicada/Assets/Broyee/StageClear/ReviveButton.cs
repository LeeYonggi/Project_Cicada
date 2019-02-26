using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveButton : MonoBehaviour {

    public HpHeart hpManager;

    public void Revive()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>().SetHp(3);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().BeInvincibleForSec(5.0f);

        Time.timeScale = 1;

        for (int i = 0; i < transform.parent.parent.parent.childCount; i++)
        {
            transform.parent.parent.parent.GetChild(i).gameObject.SetActive(true);
        }
        transform.parent.parent.GetComponent<GameClear>().Release();
    }
}
