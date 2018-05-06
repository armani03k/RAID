using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable/Decision/Beeboi/BreakFree Decision")]
public class BreakFreeDecision : Decision {

    public override bool Decide(StateController controller)
    {
        return Breakfree(controller);
    }

    bool Breakfree(StateController controller)
    {
        if (controller.GetComponent<BeeboiController>().StruggleOver())
        {
            controller.GetComponent<BeeboiController>().BreakFree();
            return true;
        }
        return false;
    }
}
