using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    [Header("Stats")]
    //HP
    public float BaseHP;
    private float trueHP;
    public float AveHP {
        get {
            return trueHP / BaseHP;
        }
    }

    //stamina
    public float BaseStamina;
    private float trueStamina;
    public float StaminaRef {
        get {
            return trueStamina;
        }
    }
    public float AveStm {
        get {
            return trueStamina / BaseStamina;
        }
    }

    [Header("UI")]
    public Image HPImg;
    public Image StmImg;

    [Header("Other")]
    public float StaminaReg;
    //public float StmRegDelay;

    void Start () {
        trueHP = BaseHP;
        trueStamina = BaseStamina;

        UIUpdate();
	}

    private void Update() {
        if (trueStamina < BaseStamina) {
            trueStamina += StaminaReg * Time.deltaTime;
            UIUpdate();
        }
    }

    public void UIUpdate() {
        HPImg.fillAmount = AveHP;
        StmImg.fillAmount = AveStm;
    }

    public void StaminaDeplete(float amt) {
        if (trueStamina > 0) trueStamina -= amt;
        if (trueStamina <= 0) trueStamina = 0;
    }
}
