using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xJoint : MonoBehaviour
{


    public int dir = 1;
    public float mult = 1;

    private Rigidbody joint;
    private bool locked;

    // Use this for initialization
    void Start()
    {
        joint = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float rotateX = Input.GetAxis("Horizontal")*dir;

        Vector3 rotation = new Vector3(rotateX, 0, 0);

        if (rotateX == 0)
        {
            joint.constraints = RigidbodyConstraints.FreezeRotationX;
        }
        else
        {
            joint.constraints = RigidbodyConstraints.None;
        }


        //joint.AddTorque(rotation * mult);
        //joint.AddForce(rotation * speed);
        joint.angularVelocity = rotation * mult;
    }
}