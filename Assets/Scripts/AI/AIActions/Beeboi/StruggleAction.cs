using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable/Actions/Beeboi/Struggle Action")]
public class StruggleAction : AiAction {

    public override void Act(StateController controller)
    {
        Struggle(controller);
    }

    void Struggle(StateController controller)
    {
        if (!controller.GetComponent<BeeboiController>().IsStruggling)
        {
            controller.GetComponent<BeeboiController>().Struggle();
        }
    }
}
