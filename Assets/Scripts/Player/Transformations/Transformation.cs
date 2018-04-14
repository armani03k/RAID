using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Transformation : MonoBehaviour {
    
    public Color BaseColor; //for prototyping

    [TextArea(0, 500)]
    public string Desc;
    protected PlayerStats pStat;

    [Header("Stamina Cost")]
    public float PrimaryCost;
    public float AbilityCost;

    public RuntimeAnimatorController anim;

    //architecture

    public virtual void UseGroundAbility() {

    }

    public virtual void UseAerialAbility() {

    }

    public virtual void UseGroundAttack() {

    }

    public virtual void UseAerialAttack() {

    }
}
