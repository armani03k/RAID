using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    public PlayerMovement pm;

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            if (!pm.GetGround) pm.OnPlatformHit(); //enable jumping + dash recoil
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            if (pm.CheckJump) pm.SetJump = false; //disable jump when you fall on ledge
        }
    }
}
