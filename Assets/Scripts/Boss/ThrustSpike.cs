using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustSpike : SubPattern {

    public float DashForce;

    bool m_launched;
	// Use this for initialization
	void Start () {
        m_bossAI = GetComponent<BossAI>();
	}
	
	// Update is called once per frame
	void Update () {

        if (m_launched)
            return;
	}

    public override IEnumerator Activate(AttackPattern caller)
    {

        m_bossAI.GetAnimator.SetBool("Attack", true);
        m_bossAI.GetAnimator.SetFloat("AttackIndex", 2);
        m_attackPatternParent = caller;
        yield return null;
    }

    public void Thrust()
    {
        m_launched = true;
        Vector2 direction = Vector2.zero;
        if(Target.transform.position.x < transform.position.x)
        {
            direction = new Vector2(-DashForce, 0);
        }
        else
            direction = new Vector2(DashForce , 0);

        m_bossAI.GetRigidBody.AddForce(direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (m_launched && m_bossAI.GetRigidBody.velocity.magnitude > 0)
        {
            m_bossAI.GetAnimator.SetBool("Attack", false);
            m_isFinished = true;

        }
            
    }

    public override void EndAttack()
    {
        m_launched = false;
        base.EndAttack();
        m_bossAI.GetRigidBody.velocity = Vector2.zero;
    }
}
