using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Stats
{
    public float m_Health;
    public float m_MaxHealth;
    public float m_AttackDamage;
}

public class BossAI : MonoBehaviour {

    
    public EnemyStat m_EnemyStat;
    public float m_AttackTransitionTime;
    public List<AttackPattern> m_attackPatterns;
    public AttackPattern m_currentAttack;


    Animator m_animator;
    Rigidbody2D m_rigidbody2D;
    int m_attackIndex;
    float m_attackTransitionTimer;

    //public Stats m_Stat;
	// Use this for initialization
	void Start () {
        m_animator = GetComponent<Animator>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_attackPatterns.AddRange(GetComponents<AttackPattern>());
	}

    //Reference to Current Units Attack Pattern. Used to manipulate outside of script such as State transitions.
    public AttackPattern CurrentAttackPattern
    {
        get { return m_currentAttack; }
        set { m_currentAttack = value; }
    }

    public Rigidbody2D GetRigidBody
    {
        get { return m_rigidbody2D; }
    }

    //Used to set parameters inside animator outside of this script.
    public Animator GetAnimator
    {
        get { return m_animator; }
    }

    //Reference to current index.
    public int AttackIndex
    {
        get { return m_attackIndex; }
        set { m_attackIndex = value; }
    }

    // Update is called once per frame
    void Update () {

        if(m_currentAttack != null && m_currentAttack.IsFinished)
        {
            m_currentAttack.EndAttack();
            m_currentAttack = null;
        }

        if (m_currentAttack == null && m_EnemyStat.HP > 0)
            m_attackTransitionTimer += Time.deltaTime;

        if (m_attackTransitionTimer > m_AttackTransitionTime && m_attackPatterns.Count != 0 && m_currentAttack == null)
        {
            m_currentAttack = m_attackPatterns[m_attackIndex];
            StartCoroutine(m_currentAttack.Attack());

            //Increment Attack index without going over the number of attack patterns detected.
            m_attackIndex = (m_attackIndex + 1) % m_attackPatterns.Count;

            m_attackTransitionTimer = 0;
        }

        if (m_EnemyStat.HP <= 0)
            Die();
	}

    //To be called upon unit death.
    public void Die()
    {
        if (m_currentAttack != null)
            m_currentAttack.StopAttack();
        m_currentAttack = null;
        m_attackPatterns.Clear();
    }

}
