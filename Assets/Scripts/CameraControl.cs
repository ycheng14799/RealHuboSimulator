using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    private Vector3 cameraPos;
    private Quaternion cameraRot;
    private Transform cameraTrans;
    private float prevX;
    private float prevY;
    private float prevZoom;

	void Start () {
        cameraPos = gameObject.GetComponent<Transform>().position;
        cameraRot = gameObject.GetComponent<Transform>().rotation;
        cameraTrans = gameObject.GetComponent<Transform>();
	}

	void Update () {
        if (Input.GetMouseButton(0))
        {
            print("ping");
            cameraPos.x = cameraPos.x + (Input.mousePosition.x - prevX) / 2;
            cameraPos.y = cameraPos.y + (Input.mousePosition.y - prevY) / 2;
        }
        if (Input.GetMouseButton(1))
        {
            print("pang");
            cameraRot.x = cameraRot.x + (Input.mousePosition.x - prevX) / 2;
            cameraRot.y = cameraRot.y + (Input.mousePosition.y - prevY) / 2;
        }
        if (Input.GetMouseButton(2))
        {
            print("pong");
            cameraPos.z = cameraPos.z + ((Input.GetAxis("Mouse ScrollWheel") - prevZoom) / 2);
        }
        gameObject.transform.position = cameraPos;
        gameObject.transform.rotation = cameraRot;
        prevX = Input.mousePosition.x;
        prevY = Input.mousePosition.y;
        prevZoom = Input.GetAxis("Mouse ScrollWheel");
    }
}
