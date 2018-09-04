using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myCameraMovement : MonoBehaviour {

    private float speed = 5;
    private float lookY;
    private float lookX;
    private Vector3 initRot;

    void Start()
    {
        initRot = gameObject.transform.rotation.eulerAngles;
    }

	void Update () {
        if (Input.GetMouseButton(0))
        {
            transform.Translate(-Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed ,-Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed , 0);
        }

            transform.Translate(0,0,Input.GetAxis("Mouse ScrollWheel")*speed);
        if (Input.GetMouseButton(1))
        {
            lookY += speed * Input.GetAxis("Mouse X");
            lookX += speed * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(lookX, -lookY,0f) + initRot;
        }
    }
}
