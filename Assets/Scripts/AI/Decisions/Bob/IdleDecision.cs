using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable/Decision/Bob/Idle Decision")]
public class IdleDecision : Decision {

    public override bool Decide(StateController controller)
    {
        return Idle(controller);
    }

    bool Idle(StateController controller)
    {
        return (controller.GetComponent<BobController>().IsHiding);
    }
}
