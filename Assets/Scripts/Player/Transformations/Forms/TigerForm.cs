using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerForm : Transformation {
    public PlayerMovement User;
    public PlayerTransform Form;
    public float DashMult;

    //public float PrimaryAttack;
    public Animator PlayerAnim;
    public float AbilityCD = 0.5f;
    private float skillTimer;

    public float AtkCD = 0.5f;
    bool m_usedAbility;
    private float atkTimer;
    int m_AttackIndex;


    private void Start() {
        pStat = Form.gameObject.GetComponent<PlayerStats>();
        skillTimer = AbilityCD;
        atkTimer = AtkCD;
    }

    private void OnEnable() {
        Form.WallRun = true;
    }

    private void OnDisable() {
        Form.WallRun = false;

        User.GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (User.GetGround) User.SetDash = true;
    }

    private void Update() {
        if (skillTimer < AbilityCD) skillTimer += Time.deltaTime;
        if (atkTimer < AtkCD) atkTimer += Time.deltaTime;
    }

    public override void UseGroundAttack() {
        if (atkTimer >= AtkCD && pStat.StaminaRef >= PrimaryCost && !Form.getWRunState) {
            PlayerAnim.SetTrigger("Attack");
            atkTimer = 0;
            pStat.StaminaDeplete(PrimaryCost);
        }
    }

    public override void UseAerialAttack() {
        if (atkTimer >= AtkCD && pStat.StaminaRef >= PrimaryCost && !Form.getWRunState) {
            PlayerAnim.SetTrigger("Attack");
            atkTimer = 0;
            pStat.StaminaDeplete(PrimaryCost);
        }
    }

    public override void UseGroundAbility() {
        if (pStat.StaminaRef >= AbilityCost) {
            if (User.GetDash) {
                m_usedAbility = true;
                StartCoroutine(Dash(User.DashDuration, true));
                m_AttackIndex = 1;
            }
        }
    }

    public override void UseAerialAbility() {
        if (pStat.StaminaRef >= AbilityCost) {
            if (User.GetDash) {
                m_usedAbility = true;
                StartCoroutine(Dash(User.DashDuration, false));
                m_AttackIndex = 2;
            }
        } 
    }

    IEnumerator Dash(float dashDur, bool grounded) {
        float dashTime = 0;
        User.SetDash = false;
        pStat.StaminaDeplete(AbilityCost);
        PlayerAnim.SetTrigger("Ability");

        while (dashTime < dashDur) {
            dashTime += Time.deltaTime;
            if (grounded) User.GetComponent<Rigidbody2D>().velocity = new Vector2(User.DashSpd * User.gameObject.transform.localScale.x * DashMult, User.GetComponent<Rigidbody2D>().velocity.y);
            else User.GetComponent<Rigidbody2D>().velocity = new Vector2(User.DashSpd * User.gameObject.transform.localScale.x * DashMult, User.DashSpd);
            yield return 0;
        }
        if(m_usedAbility)
        {
            Form.DisableTrigger(m_AttackIndex);
            m_AttackIndex = 0;
            m_usedAbility = false;
        }

        User.GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (User.GetGround) User.SetDash = true; //add enemy statement here
    }

    
}
