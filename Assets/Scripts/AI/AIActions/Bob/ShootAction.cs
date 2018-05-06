using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable/Actions/Shoot Action")]
public class ShootAction : AiAction {

    public override void Act(StateController controller)
    {
        Shoot(controller);
    }

    void Shoot(StateController controller)
    {
        if (controller.GetComponent<EnemyUnit>().AttackCoolDown())
            controller.GetComponent<BobController>().Attack();
    }
}
