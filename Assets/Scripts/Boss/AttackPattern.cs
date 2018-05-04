using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackPattern : MonoBehaviour {

    public GameObject target;
    protected BossAI m_bossAI;
    protected bool m_isFinished;
    protected bool m_distruptable;

    //Boolean to check if attack pattern is finished to exit attack state.
    public bool IsFinished
    {
        get { return m_isFinished; }
    }

    public virtual bool IsDisruptable()
    {
        return m_distruptable;
    }

    //Used to call that attack pattern is finished outside of script.
    public void FinishAttackPattern()
    {
        m_isFinished = true;
    }

    //To reset Attack pattern once attack state has exited.
    protected void ResetPattern()
    {
        m_isFinished = false;
    }

    //Called upon state exit.
    public virtual void EndAttack()
    {
        ResetPattern();
    }

    //Used when stopping attack pattern midway.
    public virtual void StopAttack()
    {
        StopAllCoroutines();
    }

    //Used to call the attack pattern in Boss Unit script.
    public abstract IEnumerator Attack();
}
