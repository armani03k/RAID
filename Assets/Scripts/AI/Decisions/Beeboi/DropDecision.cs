using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable/Decision/Beeboi/Drop Decision")]
public class DropDecision : Decision {

    public override bool Decide(StateController controller)
    {
        return Drop(controller);
    }

    bool Drop(StateController controller)
    {
        return controller.GetComponent<BeeboiController>().DropDown();
    }
}
