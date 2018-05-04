using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour {

    public bool DealDamage;
    protected bool m_enemyInRange;


    public void Attack()
    {
        DealDamage = true;
    }

    public void EndAttack()
    {
        DealDamage = false;
    }

    public bool IsEnemyInRange
    {
        get { return m_enemyInRange; }
    }

}
