using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobController : CharacterControl {

    public LayerMask m_LayerMask;
    public Direction m_ShootDirection;
    public GameObject Projectile;
    public Transform ProjectileSpawnPoint;
    public float m_ShootForce;
    public float m_AttackRange;

    GameObject m_projectile;
    bool m_hidden;
    bool m_hide;
    bool m_fire;

    public override void Start()
    {
        base.Start();
        m_hidden = true;
        m_hide = true;
    }

    public bool IsHidden
    {
        get { return m_hidden; }
    }

    public bool IsHiding
    {
        get { return m_hide; }
    }

    protected override void UpdateAnimator()
    {
        m_animator.SetBool("Attack", m_attack);
        m_animator.SetBool("Hide", m_hide);
        m_animator.SetBool("Hidden", m_hidden);
        m_animator.SetBool("Fire", m_fire);
        
    }

    public void Fire()
    {
        GetComponent<EnemyUnit>().Invulnerable = false;
        m_fire = true;
    }

    public void Show()
    {
        GetComponent<EnemyUnit>().Invulnerable = false;
        m_hide = false;
    }

    public void Emerged()
    {
        GetComponent<EnemyUnit>().Invulnerable = false;
        m_hidden = false;
        Fire();
    }

    public void Hide()
    {
        m_hide = true;
        m_fire = false;
    }

    public void Hidden()
    {
        GetComponent<EnemyUnit>().Invulnerable = true;
        m_hidden = true;
    }

    public bool TargetInSight()
    {
        RaycastHit2D hit = Physics2D.Raycast(ProjectileSpawnPoint.transform.position, new Vector2((int)m_ShootDirection, 0), m_AttackRange, m_LayerMask);
        if (hit.transform == null)
            return false;
        Debug.Log(hit.transform.gameObject);
        return (hit.transform.GetComponent<PlayerStats>() != null);
    }

    public void Shoot()
    {
        m_projectile = Instantiate(Projectile, ProjectileSpawnPoint.position, Quaternion.identity);
        m_projectile.transform.localScale = new Vector2(m_projectile.transform.localScale.x * -(int)m_ShootDirection, m_projectile.transform.localScale.y);
        m_projectile.GetComponent<BobProjectile>().SetForce(new Vector2((int)m_ShootDirection, 0) * m_ShootForce);
    }

    public override void EndAttackAnim()
    {
        base.EndAttackAnim();
    }

    void Update()
    {
        UpdateAnimator();
    }

}
