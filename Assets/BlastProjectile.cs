using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastProjectile : MonoBehaviour {


    public float BlastSpeed;
    public float Damage;
    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.down * BlastSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Animator>().SetTrigger("Disperse");

        if (collision.gameObject.GetComponent<PlayerStats>() != null)
        {
            collision.gameObject.GetComponent<PlayerStats>().TakeDmg(Damage);
        }
    }

    void Disperse()
    {
        Destroy(gameObject);
    }
}
