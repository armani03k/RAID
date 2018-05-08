using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeboiController : CharacterControl {


    [HideInInspector] public bool IsIdle;
    [HideInInspector] public bool IsDropping;
    [HideInInspector] public bool IsStruggling;
    [HideInInspector] public bool IsStuck;

    public float DropDelay;
    public float DropForce;
    public float StruggleTime;
    public float StuckTime;


    Vector2 m_originalPosition;
    float m_originalHeight;
    float m_dropTimer;
    float m_struggleTimer;
    float m_stuckTimer;

    public override void Start()
    {
        base.Start();
        IsIdle = true;
        m_originalPosition = transform.position;
        m_originalHeight = transform.position.y;
    }

    protected override void UpdateAnimator()
    {
        m_animator.SetBool("Attack", m_attack);
        m_animator.SetBool("Stuck", IsStuck);
        m_animator.SetBool("Drop", IsDropping);
        m_animator.SetBool("Struggle", IsStruggling);
        m_animator.SetBool("Idle", IsIdle);
    }

    public Vector2 OriginalPosition
    {
        get { return m_originalPosition; }
    }

    public float OriginalHeight
    {
        get { return m_originalHeight; }
    }

    public bool StopFlight()
    {
        return (transform.position.y >= m_originalHeight);
    }

    public bool StruggleOver()
    {
        if (m_struggleTimer > StruggleTime)
        {
            m_struggleTimer = 0;
            IsStruggling = false;
            return true;
        }
        m_struggleTimer += Time.deltaTime;
        return false;
    }

    public bool StuckOver()
    {
        if (m_stuckTimer > StuckTime)
        {
            m_stuckTimer = 0;
            IsStuck = false;
            return true;
        }
            
        m_stuckTimer += Time.deltaTime;
        return false;
    }

    public bool DropDown()
    {
        if (m_dropTimer > DropDelay)
        {
            m_dropTimer = 0;
            return true;
        }

        m_dropTimer += Time.deltaTime;
        return false;
    }

    public void Drop()
    {
        IsDropping = true;
        m_rigidBody.AddForce(Vector2.down * DropForce);
        GetComponentInChildren<TriggerDamager>().IsDamaging = true;
    }

    public void Stuck()
    {
        GetComponentInChildren<TriggerDamager>().IsDamaging = false;
        m_rigidBody.velocity = Vector2.zero;
        IsDropping = false;
        IsStuck = true;
    }

    public void Struggle()
    {
        IsDropping = false;
        IsStruggling = true;
    }

    public void BreakFree()
    {
        EndAttackAnim();
        IsStruggling = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsDropping || !m_attack)
            return;

        Stuck();
    }

    private void Update()
    {
        UpdateAnimator();

        if (GetComponent<EnemyUnit>().Target != null)
            IsIdle = false;
        else
            IsIdle = true;
    }

}
