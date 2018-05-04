using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable/Actions/Chase Action")]
public class ChaseAction : AiAction
{

    public override void Act(StateController controller)
    {
        Chase(controller);
    }

    Vector2 Direction(StateController controller)
    {
        Vector2 distance = controller.GetComponent<EnemyUnit>().Target.transform.position - controller.transform.position;
        return distance;
    }

    void Chase(StateController controller)
    {
        if (controller.GetComponent<EnemyUnit>().Target != null)
        {
            controller.CharControl.Move(Direction(controller));
        }
    }
}
