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

public class BossAI : MonoBehaviour, IDamagable
{

    
    public EnemyStat m_EnemyStat;
    public bool m_Invulnerable;
    public float m_AttackTransitionTime;
    public float m_InvulnerabilityTime;
    public List<AttackPattern> m_attackPatterns;
    public AttackPattern m_currentAttack;


    Animator m_animator;
    Rigidbody2D m_rigidbody2D;
    
    int m_attackIndex;
    float m_attackTransitionTimer;
    float m_health;
    float m_invulnerabilityTimer;
    float m_invulColor;
    //public Stats m_Stat;
	// Use this for initialization
	void Start () {
        m_animator = GetComponent<Animator>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_attackPatterns.AddRange(GetComponents<AttackPattern>());
        m_health = m_EnemyStat.HP;
        m_invulColor = GetComponent<SpriteRenderer>().color.r;
    }

    public float Health
    {
        get { return m_health; }
    }

    public bool InvulnerabilityOff()
    {
        m_invulnerabilityTimer += Time.deltaTime;
        return (m_invulnerabilityTimer > m_InvulnerabilityTime);
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

        if (m_health <= 0)
            Die();

        if (!m_Invulnerable)
            return;

        InvulnerableFeedback();
        if (InvulnerabilityOff())
        {
            m_Invulnerable = false;
            m_invulnerabilityTimer = 0;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
	}

    void InvulnerableFeedback()
    {
        GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 0.2f));
    }

    public void TakeDamage(float damage)
    {
        if (m_Invulnerable) return;
        if (m_health - damage > 0) m_health -= damage;
        if (m_health - damage < 0) m_health = 0;
        if (m_currentAttack != null && m_currentAttack.IsDisruptable())
            m_currentAttack.StopAttack();
        m_Invulnerable = true;
        Debug.Log(m_health);
    }

    //To be called upon unit death.
    public void Die()
    {
        if (m_currentAttack != null)
            m_currentAttack.StopAttack();
        m_currentAttack = null;
        m_attackPatterns.Clear();
        Destroy(gameObject);
    }

}
