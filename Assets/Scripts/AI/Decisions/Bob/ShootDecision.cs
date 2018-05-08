using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable/Decision/Bob/Shoot Decision")]
public class ShootDecision : Decision {

    public override bool Decide(StateController controller)
    {
        return Shoot(controller);
    }

    bool Shoot(StateController controller)
    {
        if (controller.GetComponent<BobController>().TargetInSight())
        {
            controller.GetComponent<BobController>().Show();
            return true;
        }
        return false;
    }
}
