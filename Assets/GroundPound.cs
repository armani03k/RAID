using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPound : SubPattern {

    public bool Activated;
    public float DropSpeed;
    bool m_descending;
	// Use this for initialization
	void Start () {
        m_bossAI = GetComponent<BossAI>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override IEnumerator Activate(AttackPattern caller)
    {
        m_descending = true;
        Activated = true;
        m_bossAI.GetAnimator.SetFloat("AttackIndex", -1);
        m_bossAI.GetRigidBody.AddForce(Vector2.down * DropSpeed);
        m_attackPatternParent = caller;
        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!m_descending)
            return;

        m_bossAI.GetAnimator.SetFloat("AttackIndex", -2);
        m_bossAI.GetRigidBody.velocity = Vector2.zero; 
    }

    void Pounded()
    {
        m_descending = false;
        m_attackPatternParent.FinishAttackPattern();
    }
}
