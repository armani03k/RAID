using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaedalusBlast : AttackPattern {

    public GameObject Projectile;
    public Transform SpawnPoint;
    public float FlyUpTime;
    public float FlySpeed;
    public float NumberOfShots;
    public float YLimit;

    Vector2 m_targetPosition;
    bool m_fire;
    bool m_moving;
    bool m_inPosition;
    float m_numberOfShots;
    float m_flyTimer;
	// Use this for initialization
	void Start () {
        m_bossAI = GetComponent<BossAI>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!m_inPosition)
            return;

        MoveToPosition();
        Fire();

        if (m_numberOfShots > NumberOfShots && GetComponent<GroundPound>() != null && !GetComponent<GroundPound>().Activated)
        {
            StartCoroutine(GetComponent<GroundPound>().Activate(this));
            
        }
        //else if(m_numberOfShots > NumberOfShots && GetComponent<GroundPound>() == null)
        //    m_isFinished = true;

    }

    public override IEnumerator Attack()
    {
        while(m_flyTimer < FlyUpTime)
        {
            MoveUp();
            m_flyTimer += Time.deltaTime;
            yield return null;
        }
        m_inPosition = true;
        m_moving = true;
        yield return null;
    }

    public override void EndAttack()
    {
        m_bossAI.GetAnimator.SetBool("Attack", false);
        base.EndAttack();
        m_flyTimer = 0;
        m_numberOfShots = 0;
        m_inPosition = false;
        m_moving = false;
        m_fire = false;

        if(GetComponent<GroundPound>() != null)
            GetComponent<GroundPound>().Activated = false;

    }

    void MoveUp()
    {
        m_bossAI.GetRigidBody.AddForce(Vector2.up * FlySpeed * Time.deltaTime);
    }

    void GetPosition()
    {
        m_targetPosition = target.transform.position;
        m_targetPosition.y = transform.position.y;
    }
    void MoveToPosition()
    {
        if (!m_moving || m_numberOfShots >= NumberOfShots)
            return;
        GetPosition();
        m_bossAI.GetRigidBody.MovePosition(m_targetPosition);

        if (Vector2.Distance(transform.position, m_targetPosition) < 0.2f)
        {
            m_fire = true;
        }
    }

    void Fire()
    {
        if (!m_fire || m_numberOfShots >= NumberOfShots)
            return;
        m_moving = false;
        m_bossAI.GetAnimator.SetBool("Attack", true);
        m_bossAI.GetAnimator.SetFloat("AttackIndex", 3);
    }

    void SpawnBlastProjectile()
    {
        if (m_numberOfShots < NumberOfShots)
            Instantiate(Projectile, SpawnPoint.position, Quaternion.identity);
        m_numberOfShots++;
    }

    void Fired()
    {
        m_moving = true;
        m_fire = false;
    }


}
