using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable/Actions/Beeboi/Drop Action")]
public class DropAction : AiAction {

    public override void Act(StateController controller)
    {
        Drop(controller);
    }

    void Drop(StateController controller)
    {
        if (!controller.GetComponent<BeeboiController>().IsDropping)
            controller.GetComponent<BeeboiController>().Drop();
    }
}
