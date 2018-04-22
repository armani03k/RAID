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
    private float atkTimer;

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
    }

    private void Update() {
        if (skillTimer < AbilityCD) skillTimer += Time.deltaTime;
        if (atkTimer < AtkCD) atkTimer += Time.deltaTime;
    }

    public override void UseGroundAttack() {
        if (atkTimer >= AtkCD && pStat.StaminaRef >= PrimaryCost) {
            PlayerAnim.SetTrigger("Attack");
            atkTimer = 0;
            pStat.StaminaDeplete(PrimaryCost);
        }
    }

    public override void UseAerialAttack() {
        if (atkTimer >= AtkCD && pStat.StaminaRef >= PrimaryCost) {
            PlayerAnim.SetTrigger("Attack");
            atkTimer = 0;
            pStat.StaminaDeplete(PrimaryCost);
        }
    }

    public override void UseGroundAbility() {
        if (pStat.StaminaRef >= AbilityCost) {
            if (User.GetDash) {
                StartCoroutine(Dash(User.DashDuration, true));
            }
        }
    }

    public override void UseAerialAbility() {
        if (pStat.StaminaRef >= AbilityCost) {
            if (User.GetDash) {
                StartCoroutine(Dash(User.DashDuration, false));
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

        User.GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (User.GetGround) User.SetDash = true; //add enemy statement here
    }
}
