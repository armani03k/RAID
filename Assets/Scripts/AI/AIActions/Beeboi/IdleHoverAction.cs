using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable/Actions/Beeboi/Idle Hover Action")]
public class IdleHoverAction : AiAction {


    public override void Act(StateController controller)
    {
        Fly(controller);
    }

    void Fly(StateController controller)
    {
        if (!controller.GetComponent<BeeboiController>().StopFlight())
        {
            controller.CharControl.GetRigidbody.velocity = Vector2.up;
        }
        else
        {
            controller.CharControl.GetRigidbody.velocity = Vector2.zero;
        }
            

    }
}
