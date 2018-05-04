using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable/Decision/ReChaseDecision")]
public class ReChaseDecision : Decision {

    public override bool Decide(StateController controller)
    {
        return Chase(controller);
    }

    bool Chase(StateController controller)
    {
        return (!controller.CharControl.m_attack && controller.GetComponent<EnemyUnit>().Target != null);
    }
}
