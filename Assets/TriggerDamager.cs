using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamager : MonoBehaviour {

    public EnemyUnit Enemy;
    bool m_damaging;

	// Use this for initialization
	void Start () {
        if (Enemy == null)
            Enemy = GetComponentInParent<EnemyUnit>();
	}

    public bool IsDamaging
    {
        get { return m_damaging; }
        set { m_damaging = value; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerStats>() == null || !m_damaging)
            return;

        collision.gameObject.GetComponent<PlayerStats>().TakeDmg(Enemy.EnemyType.Dmg);
    }
}
