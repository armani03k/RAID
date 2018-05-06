using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable/Actions/Bob/Emerge Action")]
public class EmergeAction : AiAction {

    public override void Act(StateController controller)
    {
        Emerge(controller);
    }

    void Emerge(StateController controller)
    {
        if(controller.GetComponent<BobController>().IsHidden && controller.GetComponent<BobController>().IsHiding)
        {
            controller.GetComponent<BobController>().Show();
        }
            
    }
}
