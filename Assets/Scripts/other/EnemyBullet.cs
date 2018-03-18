using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    public float Spd;
    public float DestroyTime;
    public EnemyStat UnitClass;

	// Update is called once per frame
	void Update () {
        Destroy(this.gameObject, DestroyTime);

        this.transform.position += Vector3.right * transform.localScale.x * Spd * Time.deltaTime;
	}

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<PlayerStats>().TakeDmg(UnitClass.Dmg);
            collision.gameObject.GetComponent<PlayerStats>().UIUpdate();
            Destroy(this.gameObject);
        }
    }
}
