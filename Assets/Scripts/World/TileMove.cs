using System.Collections;
using UnityEngine;

public class TileMove : MonoBehaviour {

    public float Spd;
    [Space(25)]
    public Transform AreaB;

    private Vector3 targetPos = Vector3.zero;
    private Vector3 originPos = Vector3.zero;

    private bool cycle;

    public GameObject PlayerRef;
    private bool isPlayerColliding = false;

	// Use this for initialization
	void Start () {
        originPos = this.transform.position;
        targetPos = AreaB.position;
	}
	
	void FixedUpdate () {
        transform.position += (Vector3.right * transform.localScale.x) * Spd * Time.deltaTime;

        if (Vector3.Distance(transform.position, AreaB.position) <= 0.1f) {
            if (!cycle) AreaB.position = originPos;
            else AreaB.position = targetPos;

            cycle = !cycle;
            Flip();
        }
	}

    void Flip() {
        var scale = this.transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.transform.position += (Vector3.right * transform.localScale.x) * Spd * Time.fixedDeltaTime; //add platform force
        }
    }
}
