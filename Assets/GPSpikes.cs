using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSpikes : MonoBehaviour {

    bool m_isDamaging;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!m_isDamaging)
            return;

        if(collision.gameObject.GetComponent<PlayerStats>() != null)
        {
            collision.gameObject.GetComponent<PlayerStats>().TakeDmg(GetComponentInParent<BossAI>().m_EnemyStat.Dmg/2);
            m_isDamaging = false;
        }
    }

    public void Activate()
    {
        
        gameObject.SetActive(true);
        GetComponent<Animator>().SetBool("Pound", true);
        m_isDamaging = true;
    }

    public void DeActivate()
    {  
        m_isDamaging = false;
        gameObject.SetActive(false);
        GetComponent<Animator>().SetBool("Pound", false);
    }
}
