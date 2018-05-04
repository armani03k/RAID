using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable/Decision/AttackDecision")]
public class AttackDecision : Decision {

    public override bool Decide(StateController controller)
    {
        return Attack(controller);
    }

    bool Attack(StateController controller)
    {
        return (controller.GetComponent<EnemyUnit>().AtkArea.IsEnemyInRange);
    }
}
