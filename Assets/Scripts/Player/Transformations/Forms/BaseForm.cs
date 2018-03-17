using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseForm : Transformation {

    public PlayerTransform Form;

    //[Header("Stamina Consumption")]
    //public float PrimaryAttack;

    private void Start() {
        pStat = Form.gameObject.GetComponent<PlayerStats>();
    }

    private void OnEnable() {
        Form.WallHop = true;
    }

    private void OnDisable() {
        Form.WallHop = false;
    }
}
