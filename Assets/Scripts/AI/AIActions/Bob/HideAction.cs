using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable/Actions/Bob/Hide Action")]
public class HideAction : AiAction {

    public override void Act(StateController controller)
    {
        Hide(controller);
    }

    void Hide(StateController controller)
    {
        if(!controller.GetComponent<BobController>().IsHidden && !controller.GetComponent<BobController>().IsHiding)
        {
            controller.GetComponent<BobController>().Hide();
            controller.GetComponent<BobController>().m_attack = false;
        }
    }
}
