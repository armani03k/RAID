using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable/Decision/Beeboi/Panic Decision")]
public class PanicDecision : Decision {

    public override bool Decide(StateController controller)
    {
        return Panic(controller);
    }

    bool Panic(StateController controller)
    {
        return (controller.GetComponent<BeeboiController>().IsStuck);
    }
}
