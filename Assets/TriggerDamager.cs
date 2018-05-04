using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamager : MonoBehaviour {

    public EnemyUnit Enemy;

	// Use this for initialization
	void Start () {
        if (Enemy == null)
            Enemy = GetComponentInParent<EnemyUnit>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerStats>() == null)
            return;

        collision.gameObject.GetComponent<PlayerStats>().TakeDmg(Enemy.EnemyType.Dmg);
    }
}
