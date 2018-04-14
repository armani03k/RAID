using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseForm : Transformation {

    public PlayerTransform Form;

    //[Header("Stamina Consumption")]
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

    private void Update() {
        if (skillTimer < AbilityCD) skillTimer += Time.deltaTime;
        if (atkTimer < AtkCD) atkTimer += Time.deltaTime;
    }

    private void OnEnable() {
        Form.WallHop = true;
    }

    private void OnDisable() {
        Form.WallHop = false;
    }

    public override void UseGroundAttack() {
        if (atkTimer >= AtkCD) {
            PlayerAnim.SetTrigger("Attack");
            atkTimer = 0;
        }
    }

    public override void UseAerialAttack() {
        if (atkTimer >= AtkCD) {
            PlayerAnim.SetTrigger("Attack");
            atkTimer = 0;
        }
    }

    public override void UseAerialAbility() {
        if (skillTimer >= AbilityCD) {
            PlayerAnim.SetTrigger("Ability");
            skillTimer = 0;
        }
    }

    public override void UseGroundAbility() {
        if (skillTimer >= AbilityCD){
            PlayerAnim.SetTrigger("Ability");
            skillTimer = 0;
        }
    }
}
