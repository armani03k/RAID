using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable/Decision/Beeboi/Struggle Decision")]
public class StruggleDecision : Decision {

    public override bool Decide(StateController controller)
    {
        return Struggle(controller);
    }

    bool Struggle(StateController controller)
    {
        return (controller.GetComponent<BeeboiController>().StuckOver());
    }
}
