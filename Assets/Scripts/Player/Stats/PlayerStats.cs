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

    [Space(25)]
    public float BarFillSpd;

    //barfill
    private Color fullCol;
    private Color lowCol;

    [Header("UI")]
    public Image HPImg;
    public Image StmImg;

    [Header("Other")]
    public float StaminaReg;
    //public float StmRegDelay;

    void Start () {
        trueHP = BaseHP;
        trueStamina = BaseStamina;

        fullCol = Color.white;
        lowCol = Color.red;

        UIUpdate();
	}

    private void Update() {
        if (trueStamina < BaseStamina) {
            trueStamina += StaminaReg * Time.deltaTime;
            UIUpdate();
        }
    }

    public void UIUpdate() {
        HPImg.fillAmount = Mathf.Lerp(HPImg.fillAmount,AveHP, BarFillSpd);
        StmImg.fillAmount = Mathf.Lerp(StmImg.fillAmount, AveStm, BarFillSpd);

        //change color
        StmImg.color = Color.Lerp(lowCol, fullCol, AveStm);
    }

    public void StaminaDeplete(float amt) {
        if (trueStamina > 0) trueStamina -= amt;
        if (trueStamina <= 0) trueStamina = 0;
    }
}
