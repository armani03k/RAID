using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinDash : AttackPattern {

    public float ChargeUpDelay;
    public float DashDelay;
    public float Speed;
    public int NumberOfBounces;

    bool m_dash;
    
    BossAI m_bossAI;
    Vector2 m_direction;
    int m_numberOfBounces;
	// Use this for initialization
	void Start () {
        m_bossAI = GetComponent<BossAI>();
	}
	
	// Update is called once per frame
	void Update () {
        if (m_numberOfBounces > NumberOfBounces)
            m_isFinished = true;

        if (m_dash)
        {
            m_bossAI.GetRigidBody.velocity = (m_direction * Speed * Time.deltaTime);
        }
            
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
        if (collision.gameObject.CompareTag("Wall") && !m_isFinished)
            DashTowards();
    }

    public void BallTransform()
    {
        m_bossAI.GetAnimator.SetFloat("AttackIndex", 1);
        DashTowards();
        m_dash = true;
    }

    public override void EndAttack()
    {
        m_bossAI.GetAnimator.SetBool("Attack", false);
        m_bossAI.GetRigidBody.velocity = Vector2.zero;
        base.EndAttack();
        Debug.Log("End");
        m_dash = false;
        m_numberOfBounces = 0;
        m_direction = Vector2.zero;

    }
}
