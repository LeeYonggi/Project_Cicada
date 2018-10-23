using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMonsterHealthBar : MonoBehaviour {

    public GameObject bossMonster;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        GetComponent<Image>().fillAmount = (float)bossMonster.GetComponent<MonsterInfo>().hp / (float)bossMonster.GetComponent<MonsterInfo>().maxHp;

    }
}
