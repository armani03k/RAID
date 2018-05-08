using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TileBounce : MonoBehaviour {

    public float TrampolineFrc;
    public bool Bounce;
    public Vector2 Dir;

    bool m_bounce;

    public void BounceUp()
    {
        m_bounce = true;
    }

    public void Retract()
    {
        Bounce = false;
        m_bounce = false;
    }

    private void Update()
    {
        GetComponent<Animator>().SetBool("SpringUp", Bounce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Bounce = true;
    }

    private void OnCollisionStay2D(Collision2D other) {

        if (!m_bounce)
            return;
        if (other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(Dir * TrampolineFrc);
            other.gameObject.GetComponent<PlayerMovement>().SetJump  = false;
        }
    }
}
