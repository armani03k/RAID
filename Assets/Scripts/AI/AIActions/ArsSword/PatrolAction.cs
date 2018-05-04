using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable/Actions/Patrol Action")]
public class PatrolAction : AiAction {

    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    void Patrol(StateController controller)
    {
        Vector2 direction = controller.wayPointList[controller.nextWayPoint].position - controller.transform.position;
        controller.CharControl.Move(direction);
        Vector2 waypoint = controller.wayPointList[controller.nextWayPoint].position;
        waypoint.y = 0;
        Vector2 mypos = controller.transform.position;
        mypos.y = 0;
        if (Vector2.Distance(mypos, waypoint) < 0.5f)
        {
            controller.nextWayPoint = Random.Range(0, controller.wayPointList.Count) % controller.wayPointList.Count;
        }
    }
}
