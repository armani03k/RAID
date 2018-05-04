using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageTrigger : MonoBehaviour {


    bool m_damaging;
    float m_damage;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!m_damaging)
            return;

        DamageDamageable(collision);
        
        //if(collision.gameObject.GetComponent<EnemyUnit>() != null)
        //{
        //    Debug.Log("eff");
        //    collision.gameObject.GetComponent<EnemyUnit>().TakeDamage(m_damage);
        //    
        //}
    }

    //Checks to see if Object hit is a damagable object.
    protected void DamageDamageable(Collider2D collision)
    {
        if (collision.gameObject.GetComponent(typeof(IDamagable)) as IDamagable == null)
            return;
        IDamagable damageable = collision.gameObject.GetComponent(typeof(IDamagable)) as IDamagable;
        damageable.TakeDamage(m_damage);
        m_damage = 0;
        m_damaging = false;
    }

    //Used to activate this specific trigger.
    public void ActivateTrigger(float damage)
    {
        m_damaging = true;
        m_damage = damage;
    }

    //Used to disable this specific trigger.
    public void DisableDamaging()
    {
        m_damaging = false;
        m_damage = 0;
    }

}
