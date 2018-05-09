using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    [Header("Stats")]
    //HP
    public float BaseHP;
    private float trueHP;
    public bool Dead;
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
    private float baseT = 0;

    [Header("UI")]
    public Image HPImg;
    public Image StmImg;

    [Header("Other")]
    public float StaminaReg;
    //public float StmRegDelay;
    public float InvulnerabilityTime;

    bool m_invulnerable;
    float m_invulnerabilityTimer;
    void Start () {
        trueHP = BaseHP;
        trueStamina = BaseStamina;

        fullCol = Color.green;
        lowCol = Color.red;

        UIUpdate();
	}

    bool InvulnerabilityOff()
    {
        if(m_invulnerabilityTimer > InvulnerabilityTime)
        {
            m_invulnerabilityTimer = 0;
            return true;
        }
        m_invulnerabilityTimer += Time.deltaTime;
        return false;
    }

    private void Update() {
        if (trueStamina < BaseStamina) {
            trueStamina += StaminaReg * Time.deltaTime;
            UIUpdate();
        }

        if (!m_invulnerable)
            return;

        if(InvulnerabilityOff())
        {
            m_invulnerable = false;
        }

        if (trueHP <= 0)
        {
            Dead = true;
            LevelEventHandler.TriggerEvent("GameOver");
            GetComponent<Animator>().SetBool("Dead", true);
        }
            
    }

    public void UIUpdate() {
        StartCoroutine(UIAnimScale(5));

        //change color
        if (HPImg == null)
            return;

        HPImg.color = Color.Lerp(lowCol, fullCol, AveHP);
    }

    public void StaminaDeplete(float amt) {
        if (trueStamina > 0) trueStamina -= amt;
        if (trueStamina <= 0) trueStamina = 0;
    }

    public void TakeDmg(float amt) {

        if (m_invulnerable) return;
        if (trueHP > 0) trueHP -= amt;
        if (trueHP <= 0) trueHP = 0;
        GetComponent<Animator>().SetTrigger("Damaged");
        m_invulnerable = true;
    }

    IEnumerator UIAnimScale(float t) {
        if (HPImg == null || StmImg == null)
            yield return null;

        while (baseT < t) {
            baseT += Time.deltaTime;
            HPImg.fillAmount = Mathf.Lerp(HPImg.fillAmount, AveHP, BarFillSpd * Time.deltaTime);
            StmImg.fillAmount = Mathf.Lerp(StmImg.fillAmount, AveStm, BarFillSpd * Time.deltaTime);
            yield return null;
        }

        baseT = 0;
    }
}
