using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable/Decision/Beeboi/ChargeUp Decision")]
public class ChargeUpDecision : Decision {

    public override bool Decide(StateController controller)
    {
        return Chargeup(controller);
    }

    bool Chargeup(StateController controller)
    {
        if (controller.GetComponent<EnemyUnit>().Target == null) return false;
        Vector2 Pos = controller.transform.position;
        Vector2 TargetPos = controller.GetComponent<EnemyUnit>().Target.transform.position;
        Pos.y = 0;
        TargetPos.y = 0;
        if (Vector2.Distance(Pos, TargetPos) < 0.1f)
        {
            controller.CharControl.Attack();
            return true;
        }
        return false;
    }
}
