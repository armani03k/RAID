using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable/Actions/Beeboi/Hover Action")]
public class HoverOverAction : AiAction {

    public override void Act(StateController controller)
    {
        Hover(controller);
    }

    Vector2 Direction(StateController controller)
    {
        Vector2 distance = controller.GetComponent<EnemyUnit>().Target.transform.position - controller.transform.position;
        distance.y = 0;
        return distance;
    }

    void Hover(StateController controller)
    {
        if (controller.GetComponent<EnemyUnit>().Target != null)
        {
            controller.CharControl.Move(Direction(controller));
        }
    }

}
