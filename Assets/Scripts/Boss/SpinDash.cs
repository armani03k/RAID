using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinDash : AttackPattern {

    public SubPattern ThrustSpike;
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
        ThrustSpike.Target = target;

    }
	
	// Update is called once per frame
	void Update () {
        if (m_numberOfBounces > NumberOfBounces && m_bossAI.m_EnemyStat.HP > m_bossAI.m_EnemyStat.MaxHP/2 && !m_taunt)
        {
            Taunt();
        }

        if (m_dash)
        {
            m_bossAI.GetRigidBody.velocity = (m_direction.normalized * Speed * Time.deltaTime);
        }

        if (ThrustSpike != null && ThrustSpike.IsFinished && !m_taunt)
        {
            Taunt();
            ThrustSpike.EndAttack();
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

    public override bool IsDisruptable()
    {
        return (m_bossAI.CurrentAttackPattern == this && m_numberOfBounces <= 1);
    }

    ///Function to call to move boss in first anim tranformation and for all collisions that come after.
    void DashTowards()
    {
        m_direction = target.transform.position - transform.position;
        if (m_direction.magnitude < 1)
            m_direction = Vector2.one;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!m_isFinished && m_bossAI.CurrentAttackPattern == this)
        {
            DashTowards();
            m_numberOfBounces++;
        }
            


        if (ThrustSpike == null)
            return;

        if (m_numberOfBounces > NumberOfBounces && m_bossAI.Health < m_bossAI.m_EnemyStat.MaxHP / 2 && m_dash)
        {
            m_dash = false;
            m_bossAI.GetRigidBody.velocity = Vector2.zero;
            StartCoroutine(ThrustSpike.Activate(this));
        }
            
    }

    ///Function called in Spin Dash Animation to transfer current anim to ball form anim.
    public void BallTransform()
    {
        m_bossAI.GetAnimator.SetFloat("AttackIndex", 1);
        DashTowards();
        m_dash = true;
    }

    ///Checks if the taunt time elapsed is over.
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

    public override void StopAttack()
    {
        base.StopAttack();
        m_direction = Vector2.zero;
        m_bossAI.GetRigidBody.velocity = Vector2.zero;
        m_dash = false;
        m_isFinished = true;
        m_numberOfBounces = 0;
        m_bossAI.GetAnimator.SetBool("Attack", false);
    }

    public override void EndAttack()
    {
        m_bossAI.GetAnimator.SetBool("Taunt", false);
        base.EndAttack();
        m_taunt = false;
        m_dash = false;
        m_numberOfBounces = 0;
        m_tauntTimer = 0;
        m_direction = Vector2.zero;
        m_bossAI.GetRigidBody.velocity = Vector2.zero;

    }
}
