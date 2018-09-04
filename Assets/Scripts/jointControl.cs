using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jointControl : MonoBehaviour {


    public float speed;

    private Rigidbody rb;
    private bool locked;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float rotateX = Input.GetAxis("Horizontal");
        float rotateY = Input.GetAxis("Vertical");

        Vector3 rotation = new Vector3(rotateX, 0, rotateY);
        

        //rb.AddTorque(rotation * speed);
        //rb.AddForce(rotation * speed);
        rb.angularVelocity = rotation * speed;
    }
}