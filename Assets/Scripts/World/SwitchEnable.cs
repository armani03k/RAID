using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchEnable : MonoBehaviour {

    public List<GameObject> ItemsToEnable;
    [Space(20)]public float Duration = 20.0f;

    private bool isActive = false;
    private float activeTimer = 0;
    Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
        foreach (GameObject g in ItemsToEnable) {
            g.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
		if (isActive) {
            foreach (GameObject g in ItemsToEnable) {
                g.SetActive(true);
            }

            if (activeTimer < Duration) activeTimer += Time.deltaTime;
        }

        if (activeTimer >= Duration) {
            foreach (GameObject g in ItemsToEnable) {
                g.SetActive(false);
            }
            activeTimer = 0;
            isActive = false;
            anim.SetTrigger("Switch");
        }
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if (!isActive) anim.SetTrigger("Switch");
            isActive = true;
        }
    }
}
