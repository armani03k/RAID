using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour {

    public List<GameObject> ObjectsToActivate;
    public List<GameObject> ObjectsToDisable;

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            foreach (GameObject go in ObjectsToActivate) go.SetActive(true);
            foreach (GameObject go in ObjectsToDisable) go.SetActive(false);
        }
    }
}
