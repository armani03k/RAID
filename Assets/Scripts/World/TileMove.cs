using System.Collections;
using UnityEngine;

public class TileMove : MonoBehaviour {

    public float Spd;
    [Space(25)]
    public Transform AreaB;

    private Vector3 targetPos = Vector3.zero;
    private Vector3 originPos = Vector3.zero;

    public Vector2 Dir;

    private bool cycle;

	// Use this for initialization
	void Start () {
        originPos = this.transform.position;
        targetPos = AreaB.position;
	}
	
	void FixedUpdate () {
        transform.position = Vector3.MoveTowards(transform.position, AreaB.transform.position, Spd * Time.deltaTime);

        if (Vector3.Distance(transform.position, AreaB.position) <= 0.1f) {
            if (!cycle) AreaB.position = originPos;
            else AreaB.position = targetPos;

            cycle = !cycle;
        }
	}

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.transform.SetParent(this.gameObject.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.transform.SetParent(null);
        }
    }


}
