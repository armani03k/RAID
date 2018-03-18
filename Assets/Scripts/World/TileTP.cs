using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTP : MonoBehaviour {

    public Transform RespawnPt;

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Ground") && collision.gameObject.GetComponent<TileMove>() != null) {
            collision.gameObject.transform.position = RespawnPt.position;
        }
    }
}
