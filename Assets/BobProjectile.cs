using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobProjectile : MonoBehaviour {

    public float Damage;
    Vector2 m_force;

    public void SetForce(Vector2 force)
    {
        m_force = force;
    }

    public void Move()
    {
        GetComponent<Rigidbody2D>().AddForce(m_force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Animator>().SetBool("Disperse", true);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        if (collision.GetComponent<PlayerStats>() == null)
            return;

        collision.GetComponent<PlayerStats>().TakeDmg(Damage);
    }

    public void Disperse()
    {
        Destroy(gameObject);
    }

}
