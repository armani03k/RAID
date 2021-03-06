﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(CharacterControl))]
public class StateController : MonoBehaviour {

    public State currentState;
    public State remainState;

    [HideInInspector] public float stateTimeElapsed;
    [SerializeField] protected CharacterControl m_CharControl;
    public List<Transform> wayPointList;
    public int nextWayPoint;
    // Use this for initialization
    void Start () {
        m_CharControl = GetComponent<CharacterControl>();
	}

    public CharacterControl CharControl
    {
        get { return m_CharControl; }
    }

    ///A time based Transitioning Condition.
    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    /// <summary>
    /// Transitions to then next State if not remain in current state.
    /// </summary>
    /// <param name="nextState"></param>
    public virtual void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
            OnExitState();
        }
    }

    protected virtual void OnExitState()
    {
        stateTimeElapsed = 0;
    }

    // Update is called once per frame
    protected virtual void Update () {
        currentState.UpdateState(this);
    }



}
