using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurret : MonoBehaviour {

    public EnemyStat UnitClass;
    public GameObject Bullet;
    public GameObject BulletSpawnPt;
    public float ReloadTime;
    private float shotTimer = 0;

    Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
    }

	void LateUpdate () {
        if (shotTimer < ReloadTime) shotTimer += Time.deltaTime;
        else {
            anim.SetTrigger("Shoot");
            shotTimer = 0;
        }
	}

    public void ShootForward() {
        var bullet = Instantiate(Bullet, BulletSpawnPt.transform.position, BulletSpawnPt.transform.localRotation);
        //flip bullet facing direction
        var temp = bullet.transform.localScale;
        temp.x *= -transform.localScale.x;
        bullet.transform.localScale = temp;
    }
}
