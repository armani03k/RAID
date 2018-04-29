using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamager : MonoBehaviour {

    public BossAI Boss;
	// Use this for initialization
	void Start () {
        if (Boss == null)
            Boss = GetComponentInParent<BossAI>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerStats>() == null)
            return;

        collision.gameObject.GetComponent<PlayerStats>().TakeDmg(Boss.m_EnemyStat.Dmg);
    }
}
