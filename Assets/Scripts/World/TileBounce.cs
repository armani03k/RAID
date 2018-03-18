using System.Collections;
using UnityEngine;

public class TileBounce : MonoBehaviour {

    public float TrampolineFrc;
    public Vector2 Dir;

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(Dir * TrampolineFrc);
            other.gameObject.GetComponent<PlayerMovement>().SetJump  = false;
        }
    }
}
