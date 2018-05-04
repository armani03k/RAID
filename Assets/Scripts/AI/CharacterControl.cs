using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour {

    Rigidbody2D m_rigidBody;
    Animator m_animator;

    protected bool m_damaged;
    public bool m_attack;
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }

    ///Updates the Animator Parameters continuously.
    protected virtual void UpdateAnimator()
    {
        m_animator.SetBool("Attack", m_attack);
        m_animator.SetFloat("Move", m_rigidBody.velocity.magnitude);
        m_animator.SetBool("Damaged", m_damaged);
    }

    ///Returns the Animator for animation triggering outside of this class.
    public Animator GetAnimator
    {
        get { return m_animator; }
    }

    ///Returns the Rigidbody2D for Physics manipulation outside of this class.
    public Rigidbody2D GetRigidbody
    {
        get { return m_rigidBody; }
    }

    public virtual void Attack()
    {
        m_attack = true;
    }

    public virtual void EndAttackAnim()
    {
        m_attack = false;
        GetComponent<EnemyUnit>().ResetAtkCooldown();
    }

    ///To Move the Character.
    public virtual void Move(Vector2 direction)
    {
        Vector2 move = direction.normalized * GetComponent<EnemyUnit>().MoveSpd * Time.deltaTime;
        m_rigidBody.velocity = move;
    }

    void Update()
    {
        UpdateAnimator();
    }

}
