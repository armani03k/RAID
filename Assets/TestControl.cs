using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += movement/10;
    }

    // Update is called once per frame
    void Update () {
        Move();
	}
}
