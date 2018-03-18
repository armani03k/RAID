using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurret : MonoBehaviour {

    public EnemyStat UnitClass;
    public GameObject Bullet;
    public GameObject BulletSpawnPt;
    public float ReloadTime;
    private float shotTimer = 0;

	void LateUpdate () {
        if (shotTimer < ReloadTime) shotTimer += Time.deltaTime;
        else {
            Instantiate(Bullet, BulletSpawnPt.transform.position, transform.localRotation);
            shotTimer = 0;
        }
	}
}
