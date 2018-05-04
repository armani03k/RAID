using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAreaTriggers : AttackArea {


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerStats>() == null)
            return;

        m_enemyInRange = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerStats>() == null || !DealDamage)
            return;

        collision.gameObject.GetComponent<PlayerStats>().TakeDmg(GetComponentInParent<EnemyUnit>().EnemyType.Dmg);
        DealDamage = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerStats>() == null)
            return;

        m_enemyInRange = false;
    }
}
