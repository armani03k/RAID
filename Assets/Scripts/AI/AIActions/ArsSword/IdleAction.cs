using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable/Actions/Idle Action")]
public class IdleAction : AiAction {

    public override void Act(StateController controller)
    {
        controller.CharControl.GetRigidbody.velocity = Vector2.zero;
        return;
    }
}
