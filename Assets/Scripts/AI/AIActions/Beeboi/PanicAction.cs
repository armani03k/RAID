using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable/Actions/Beeboi/Panic Action")]
public class PanicAction : AiAction {

    public override void Act(StateController controller)
    {
        Panic(controller);
    }

    void Panic(StateController controller)
    {
        if (!controller.GetComponent<BeeboiController>().IsStuck)
        {
            controller.GetComponent<BeeboiController>().Stuck();
        }
    }
}
