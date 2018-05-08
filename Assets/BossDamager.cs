using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamager : MonoBehaviour {

    public BossAI Boss;
    public float Damage;
	// Use this for initialization
	void Start () {
        if (Boss == null)
            Boss = GetComponentInParent<BossAI>();
        Damage = Boss.m_EnemyStat.Dmg;

    }

    public void SetDamage(float damage)
    {
        Damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<PlayerStats>() == null || Boss.m_Invulnerable)
            return;

        if (!Boss.m_Invulnerable)
        collision.gameObject.GetComponent<PlayerStats>().TakeDmg(Damage);
    }
}
