using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    protected bool m_attackCooldown;
    [Header("Unit Stats")]
    public float IdleDelay;
    protected float delayTimer = 0.0f;
    protected float m_patrolDelayTimer;
    protected float m_attackDelayTimer;
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
        m_patrolDelayTimer += Time.deltaTime;
        if (m_patrolDelayTimer > AiStat.PatrolPointSwitchDelay)
        {
            m_patrolDelayTimer = 0;
            return true;
        }
        return false;
    }

    //To be Called in Animation Event.
    public void DamageNow()
    {
        AtkArea.Attack();
    }

    public bool AttackCoolDown()
    {
        m_attackDelayTimer += Time.deltaTime;
        if(m_attackDelayTimer > AiStat.AttackSpeed)
        {
            return true;
        }
        return false;
    }

    public void ResetAtkCooldown()
    {
        m_attackDelayTimer = 0;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Wtf");
        if (m_health - damage > 0) m_health -= damage;
        if (m_health - damage < 0) m_health = 0;
    }

    //protected virtual void Update() {
    //    switch (ActiveState) {
    //        case UnitStates.Idle: OnIdle(); break;
    //        case UnitStates.Patrol: OnPatrol(); break;
    //        case UnitStates.Chase: OnChase(); break;
    //        case UnitStates.Attack: OnAttack(); break;
    //        case UnitStates.Death: OnDeath(); break;
    //    }
    //}

    //protected virtual void OnIdle() {

    //}

    //protected virtual void OnPatrol() {

    //}

    //protected virtual void OnChase() {

    //}

    //protected virtual void OnAttack() {

    //}

    //protected virtual void OnDeath() {

    //}

    protected virtual void Flip() {
        var temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;
    }

    private void Update()
    {
        if (m_health <= 0)
            Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, AiStat.ChaseAreaRdius);
    }
}
