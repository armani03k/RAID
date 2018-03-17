using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class EnemyUnit : MonoBehaviour {

    public enum UnitStates {
        Idle,
        Patrol,
        Chase,
        Attack,
        Death
    }

    public EnemyStat EnemyType;
    public UnitStates ActiveState;
    public float MoveSpd;

    [Header("Unit Stats")]
    public float IdleDelay;
    protected float delayTimer = 0.0f;
    SpriteRenderer uSprite;

    protected virtual void Start() {
        uSprite = GetComponent<SpriteRenderer>();
        uSprite.color = EnemyType.BaseSkin;
    }

    protected virtual void Update() {
        switch (ActiveState) {
            case UnitStates.Idle: OnIdle(); break;
            case UnitStates.Patrol: OnPatrol(); break;
            case UnitStates.Chase: OnChase(); break;
            case UnitStates.Attack: OnAttack(); break;
            case UnitStates.Death: OnDeath(); break;
        }
    }

    protected virtual void OnIdle() {

    }

    protected virtual void OnPatrol() {

    }

    protected virtual void OnChase() {

    }

    protected virtual void OnAttack() {

    }

    protected virtual void OnDeath() {

    }

    protected virtual void Flip() {
        var temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;
    }
}
