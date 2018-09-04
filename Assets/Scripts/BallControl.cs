using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour {

    public float speed;

    private Vector3 home;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        home = new Vector3(0,1,0);
    }


    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("returnToCenter"))
        {
            rb.MovePosition(home);
        }
    }
}
