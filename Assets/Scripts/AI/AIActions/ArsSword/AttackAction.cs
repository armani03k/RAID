using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable/Actions/Attack Action")]
public class AttackAction : AiAction {

    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    void Attack(StateController controller)
    {
        controller.CharControl.GetRigidbody.velocity = Vector2.zero;
        controller.CharControl.Attack();
    }

}
