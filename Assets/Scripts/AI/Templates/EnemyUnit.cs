using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Left = -1,
    Right = 1
}

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyUnit : MonoBehaviour, IDamagable
{

    public enum UnitStates {
        Idle,
        Patrol,
        Chase,
        Attack,
        Death
    }

    public EnemyStat EnemyType;
    public AIStats AiStat;
    public AttackArea AtkArea;
    public GameObject Target;
    //public UnitStates ActiveState;
    public float MoveSpd;
    public Direction m_direction;

    [SerializeField]protected bool m_attackCooldown;
    protected bool m_patrolPointReached;
    [Header("Unit Stats")]
    public float IdleDelay;
    protected float delayTimer = 0.0f;
    protected float m_patrolDelayTimer;
    protected float m_attackDelayTimer;
    protected float m_flipValue;
    protected float m_health;
    SpriteRenderer uSprite;
    

    protected virtual void Start() {
        uSprite = GetComponent<SpriteRenderer>();
        uSprite.color = EnemyType.BaseSkin;
        m_health = EnemyType.HP;
    }

    /// <summary>
    /// Determines when to switch to a different Waypoint.
    /// </summary>
    /// <returns></returns>
    public bool PatrolSwitch()
    {
        
        if (m_patrolDelayTimer > AiStat.PatrolPointSwitchDelay)
        {
            m_patrolDelayTimer = 0;
            m_patrolPointReached = false;
            return true;
        }
        m_patrolDelayTimer += Time.deltaTime;
        return false;
    }

    public bool IsPatrolPointReached
    {
        get { return m_patrolPointReached; }
        set { m_patrolPointReached = value; }
    }

    public bool IsAttackCoolDown
    {
        get { return m_attackCooldown; }
    }

    //To be Called in Animation Event.
    public void DamageNow()
    {
        AtkArea.Attack();
    }

    public bool AttackCoolDown()
    {
        if(m_attackDelayTimer > AiStat.AttackSpeed)
        {
            m_attackCooldown = false;
            return true;
        }
        m_attackDelayTimer += Time.deltaTime;
        return false;
    }

    public void ResetAtkCooldown()
    {
        m_attackDelayTimer = 0;
        m_attackCooldown = true;
    }

    public void TakeDamage(float damage)
    {
        if (m_health - damage > 0) m_health -= damage;
        if (m_health - damage < 0) m_health = 0;
    }

    protected virtual void Flip() {
        var temp = transform.localScale;
        temp.x = m_flipValue;
        transform.localScale = temp;
    }

    private void Update()
    {
        if (m_health <= 0)
            Destroy(gameObject);

        if (Target == null)
            return;

        if (Target.transform.position.x < transform.position.x)
            m_flipValue = 1;
        else
            m_flipValue = -1;

        Flip();
    }
    
    private void OnDrawGizmos()
    {
        if(AiStat != null)
            Gizmos.DrawWireSphere(transform.position, AiStat.ChaseAreaRdius);
    }
}
