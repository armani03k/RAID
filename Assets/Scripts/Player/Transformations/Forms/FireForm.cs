using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireForm : Transformation {

    public PlayerTransform Form;
    [Header("Stats")]
    public float BurstFrc; //upward burst fire force
    public float FTDelay; //flamethrower delay
    //private float delayTimer = 0;

    [SerializeField]
    private bool isHolding = false; //button held?

    private void Start() {
        pStat = Form.gameObject.GetComponent<PlayerStats>();
    }

    private void OnEnable() {
        Form.FlameOn = true;
    }

    private void OnDisable() {
        Form.FlameOn = false;
    }

    /*
    private void Update() {
        if (Input.GetButtonUp("Ability") && !isHolding && delayTimer < FTDelay 
            && Form.FlameOn && Form.GetComponent<PlayerMovement>().GetGround) Fireball();

        if (Input.GetButtonUp("Ability") && isHolding) StopFT();
    }
    */

    public override void UseGroundAbility() {
        if (pStat.StaminaRef >= AbilityCost) {
            pStat.StaminaDeplete(AbilityCost);
            Form.GetComponent<Rigidbody2D>().velocity = Vector2.up * BurstFrc;
        }
    }

    public override void UseAerialAbility() {
        if (pStat.StaminaRef >= AbilityCost) {
            pStat.StaminaDeplete(AbilityCost);
            Form.GetComponent<Rigidbody2D>().velocity = Vector2.up * BurstFrc;
        }
    }

    void Fireball() {
        //delayTimer = 0;
        Debug.Log("Fireball thrown. . .");
    }

    void Flamethrower() {
        if (!isHolding) {
            isHolding = true;
            Debug.Log("Using FlameThrower. . .");
        }
    }

    void StopFT() {
        if (isHolding) {
            isHolding = false;
            //delayTimer = 0;
            Debug.Log("Stopping FlameThrower. . .");
        }
    }

}
