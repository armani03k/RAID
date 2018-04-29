using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SubPattern : MonoBehaviour {

    public GameObject Target;
    protected AttackPattern m_attackPatternParent;
    protected BossAI m_bossAI;
    protected bool m_isFinished;
	// Use this for initialization
	void Start () {
		
	}
	
    public bool IsFinished
    {
        get { return m_isFinished; }
    }

    //Used to call that attack pattern is finished outside of script.
    public void FinishAttackPattern()
    {
        m_isFinished = true;
    }

    void ResetPattern()
    {
        m_isFinished = false;
    }

    public virtual void EndAttack()
    {
        ResetPattern();
    }

    public virtual void StopAttack()
    {
        StopAllCoroutines();
    }

    public abstract IEnumerator Activate(AttackPattern caller);
	// Update is called once per frame
	void Update () {
		
	}
}
