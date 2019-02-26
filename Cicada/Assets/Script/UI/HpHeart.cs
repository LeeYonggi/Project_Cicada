using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpHeart : MonoBehaviour {

    public List<GameObject> m_lHpUI = new List<GameObject>();
    public GameObject hpPrefeb;
    public GameObject player;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < m_lHpUI.Count; i++)
        {
             GameObject obj = Instantiate(m_lHpUI[i], transform.position, Quaternion.identity);
             obj.transform.parent = transform;
             obj.transform.Translate(new Vector2(i * 140, 0));
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(player.GetComponent<PlayerInfo>().MaxHp > m_lHpUI.Count)
        {
            for(int i = m_lHpUI.Count; i < player.GetComponent<PlayerInfo>().MaxHp; i++)
            {
                GameObject obj = Instantiate(hpPrefeb, transform.position, Quaternion.identity);
                m_lHpUI.Add(obj);
                obj.transform.parent = transform;
                obj.transform.Translate(new Vector2(i * 140, 0));
            }
        }
        for(int i = m_lHpUI.Count - 1; i > -1; --i)
        {
            if (player.GetComponent<PlayerInfo>().Hp <= i)
                m_lHpUI[i].GetComponent<Heart>().IsDamaged = true;
            else
                m_lHpUI[i].GetComponent<Heart>().IsDamaged = false;
        }
    }
}
