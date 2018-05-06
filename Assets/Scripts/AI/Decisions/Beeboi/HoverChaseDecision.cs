using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable/Decision/Beeboi/ChaseOver Decision")]
public class HoverChaseDecision : Decision {


    public override bool Decide(StateController controller)
    {
        return Chase(controller);
    }

    bool Chase(StateController controller)
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(controller.transform.position, controller.GetComponent<EnemyUnit>().AiStat.ChaseAreaRdius);

        foreach (Collider2D col in cols)
        {
            if (col.GetComponent<PlayerStats>() != null && controller.GetComponent<BeeboiController>().StopFlight())
            {
                Debug.Log("FFE");
                controller.GetComponent<EnemyUnit>().Target = col.gameObject;
                return true;
            }

        }
        return false;
    }
}
