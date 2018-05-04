using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable/Decision/PatrolDecision")]
public class PatrolDecision : Decision {

    
    public override bool Decide(StateController controller)
    {
        return Patrol(controller);
    }

    bool Patrol(StateController controller)
    {
        return (controller.GetComponent<EnemyUnit>().PatrolSwitch());
    }
}
