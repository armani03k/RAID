﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinDash : AttackPattern {

    public float ChargeUpDelay;
    public float DashDelay;
    public float Speed;
    public float TauntTime;
    public int NumberOfBounces;

    bool m_dash;
    bool m_taunt;
    
    Vector2 m_direction;
    int m_numberOfBounces;
    float m_tauntTimer;
	// Use this for initialization
	void Start () {
        m_bossAI = GetComponent<BossAI>();
	}
	
	// Update is called once per frame
	void Update () {
        if (m_numberOfBounces > NumberOfBounces && m_bossAI.m_EnemyStat.HP > m_bossAI.m_EnemyStat.MaxHP/2 && !m_taunt)
        {
            Debug.Log("ff");
            Taunt();
        }

        if (m_dash)
        {
            m_bossAI.GetRigidBody.velocity = (m_direction * Speed * Time.deltaTime);
        }

        if (IsDoneTaunting())
            m_isFinished = true;

    }

    public override IEnumerator Attack()
    {
        
        new WaitForSeconds(ChargeUpDelay);
        m_bossAI.GetAnimator.SetBool("Attack", true);
        m_bossAI.GetAnimator.SetFloat("AttackIndex", 0);
        yield return null;
    }



    void DashTowards()
    {
        m_direction = target.transform.position - transform.position;
        m_numberOfBounces++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") && !m_isFinished && m_bossAI.CurrentAttackPattern == this)
            DashTowards();


        if (m_numberOfBounces > NumberOfBounces && m_bossAI.m_EnemyStat.HP < m_bossAI.m_EnemyStat.MaxHP / 2)
        {
            m_dash = false;
            m_bossAI.GetRigidBody.velocity = Vector2.zero;
            StartCoroutine(GetComponent<ThrustSpike>().Attack());
        }

        if (GetComponent<ThrustSpike>().IsFinished == true)
        {
            GetComponent<ThrustSpike>().EndAttack();
            Taunt();
        }
            
    }

    public void BallTransform()
    {
        m_bossAI.GetAnimator.SetFloat("AttackIndex", 1);
        DashTowards();
        m_dash = true;
    }

    bool IsDoneTaunting()
    {
        if (!m_taunt)
            return false;

        m_tauntTimer += Time.deltaTime;
        return (m_tauntTimer > TauntTime);
    }

    public void Taunt()
    {
        m_bossAI.GetAnimator.SetBool("Attack", false);
        m_bossAI.GetRigidBody.velocity = Vector2.zero;
        m_dash = false;
        m_bossAI.GetAnimator.SetBool("Taunt", true);
        m_taunt = true;

    }

    public override void EndAttack()
    {
        m_bossAI.GetAnimator.SetBool("Taunt", false);
        base.EndAttack();
        
        m_numberOfBounces = 0;
        m_tauntTimer = 0;
        m_direction = Vector2.zero;

    }
}
