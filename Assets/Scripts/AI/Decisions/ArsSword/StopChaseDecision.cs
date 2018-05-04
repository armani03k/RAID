using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable/Decision/StopChaseDecision")]
public class StopChaseDecision : Decision {

    public override bool Decide(StateController controller)
    {
        return Return(controller);
    }

    bool Return(StateController controller)
    {
        if (Vector2.Distance(controller.transform.position, controller.GetComponent<EnemyUnit>().Target.transform.position) > controller.GetComponent<EnemyUnit>().AiStat.ChaseStopDistance)
        {
            controller.GetComponent<EnemyUnit>().Target = null;
            controller.CharControl.GetRigidbody.velocity = Vector2.zero;
            return true;
        }
        return false;
    }
}
