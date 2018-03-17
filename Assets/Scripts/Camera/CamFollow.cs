using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour {

    public Transform Target;
    public float Damp = 0.125f;

    public Vector3 MinBounds;
    public Vector3 MaxBounds;

    public Vector3 Offset;
	
	void LateUpdate () {
        var offPos = Target.position + Offset;
        var SmoothPos = Vector3.Lerp(transform.position, offPos, Damp * Time.deltaTime);

        //camera bounds
        if(Target.GetComponent<PlayerMovement>() != null) {
            transform.position = new Vector3(Mathf.Clamp(SmoothPos.x, MinBounds.x, MaxBounds.x), Mathf.Clamp(SmoothPos.y, MinBounds.y, MaxBounds.y), SmoothPos.z);
        }

        //transform.LookAt(new Vector3(Target.position.x, transform.position.y, Target.position.z));
	}
}
