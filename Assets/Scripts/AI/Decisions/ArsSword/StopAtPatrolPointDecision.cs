using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable/Decision/StopAtPatrolPoint Decision")]
public class StopAtPatrolPointDecision : Decision {

    public override bool Decide(StateController controller)
    {
        return StopAtPoint(controller);
    }

    bool StopAtPoint(StateController controller)
    {
        return (controller.GetComponent<EnemyUnit>().IsPatrolPointReached);
    }
}
