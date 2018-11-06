using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpHeart : MonoBehaviour {

    public List<GameObject> m_lHpUI = new List<GameObject>();
    public GameObject player;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < m_lHpUI.Count; i++)
        {
             GameObject obj = Instantiate(m_lHpUI[i], transform.position, Quaternion.identity);
             obj.transform.parent = transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
        //player.GetComponent<PlayerInfo>().

    }
}
