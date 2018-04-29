using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastProjectile : MonoBehaviour {


    public float BlastSpeed;

    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.down * BlastSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Animator>().SetTrigger("Disperse");
    }

    void Disperse()
    {
        Destroy(gameObject);
    }
}
