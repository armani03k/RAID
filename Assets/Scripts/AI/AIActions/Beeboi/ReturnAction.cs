using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable/Actions/Beeboi/Return Action")]
public class ReturnAction : AiAction {

    public override void Act(StateController controller)
    {
        Return(controller);
    }

    Vector2 Direction(StateController controller)
    {
        Vector2 distance = controller.GetComponent<BeeboiController>().OriginalPosition - (Vector2)controller.transform.position;
        return distance;
    }

    void Return(StateController controller)
    {
        if (controller.GetComponent<EnemyUnit>().Target != null)
        {
            controller.CharControl.Move(Direction(controller));
        }
    }
}
