using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerMovement))]
public class BetterJump : MonoBehaviour {

    public float FallMultiplier = 2.5f;
    public float FallDefault;
    Rigidbody2D rb;
    PlayerMovement pm;
    
	void Start () {
        FallMultiplier = FallDefault;
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMovement>();
	}
	
	void Update () {
        //gravity proc
		if (rb.velocity.y != 0 && ! pm.GetGround && (rb.velocity.y > 1 || rb.velocity.y < -1)) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (FallMultiplier) * Time.deltaTime;
        }
	}

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Ground")) pm.SetGround = false; //disable jump
    }
}
