using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class HUBOUnivJoint2 : MonoBehaviour
{

    public int direction = 1;
    public string motorTag;
    public string txtFile = "motorLocsSpeeds.txt";

    private float angle;
    private float prevAng;
    private int textInd;
    private string txtIn;
    private Rigidbody joint;
    private ConfigurableJoint configJoint;

    private int velInd;
    private int posInd;
    private float vel;
    private Vector3 rotation;

    private string velString;
    private string posString;
    private bool changed;

    void Start()
    {
        TextAsset txtAssets = (TextAsset)Resources.Load(txtFile);
        txtIn = txtAssets.text;
        
        joint = GetComponent<Rigidbody>();
        motorTag = gameObject.tag;
        motorTag = motorTag.Substring(5, motorTag.Length - 5);
        //if (gameObject.name != "Body_Torso")
        //{
        //    configJoint = GetComponent<ConfigurableJoint>();
        //    if (configJoint.axis.x != 0)
        //    {
        //        motorTag = (motorTag.Substring(0, 2) + "P");
        //        print("ping");
        //    }
        //    else if (configJoint.axis.y != 0)
        //    {
        //        motorTag = (motorTag.Substring(0, 2) + "R");
        //        print("pang");
        //    }
        //    else if (configJoint.axis.z != 0)
        //    {
        //        motorTag = (motorTag.Substring(0, 2) + "Y");
        //        print("pong");
        //    }
        //}



        if (motorTag.Contains("L") && motorTag.Contains("R"))
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        joint.transform.localPosition = Vector3.zero;
        {
            TextAsset txtAssets = (TextAsset)Resources.Load(txtFile);
            txtIn = txtAssets.text;

            textInd = txtIn.IndexOf(motorTag);

            velInd = textInd + motorTag.Length + 1;
            posInd = textInd + motorTag.Length + 5;

            velString = txtIn.Substring(velInd, 4); //should be 4 chars including .
            posString = txtIn.Substring(posInd, 4); //must be 4 chars with the first a 0 or negative sign and between -180 and 180

            //print(velString);

            if (velString.Length > 1)
            {
                vel = float.Parse(velString);
                angle = float.Parse(posString) * direction;
            }
            else { }

            vel = vel * Mathf.Rad2Deg * Time.deltaTime;
        }

        {
            if (angle > prevAng)
            {
                if (prevAng + vel > angle)
                {
                    vel = angle - prevAng;
                }
                changed = true;
            }
            else if (angle < prevAng)
            {
                if (prevAng - vel < angle)
                {
                    vel = -1 * Mathf.Abs(angle - prevAng);
                }
                else
                {
                    vel = vel * -1;
                }
                changed = true;
            }
            else
            {
                vel = 0;
            }

            if (motorTag.Substring(2, 1) == "p" || motorTag.Substring(2, 1) == "P")
            {
                transform.Rotate(new Vector3(vel, 0, 0));
                joint.constraints = RigidbodyConstraints.None;
                joint.constraints = RigidbodyConstraints.FreezeRotationX;
            }
            else if (motorTag.Substring(2, 1) == "y" || motorTag.Substring(2, 1) == "Y")
            {
                transform.Rotate(new Vector3(0, vel, 0));
                joint.constraints = RigidbodyConstraints.None;
                joint.constraints = RigidbodyConstraints.FreezeRotationY;
            }
            else if (motorTag.Substring(2, 1) == "r" || motorTag.Substring(2, 1) == "R")
            {
                transform.Rotate(new Vector3(0, 0, vel));
                joint.constraints = RigidbodyConstraints.None;
                joint.constraints = RigidbodyConstraints.FreezeRotationZ;
            }
            else { joint.constraints = RigidbodyConstraints.FreezeRotation; }///////currently used to freeze uncontrolled joints
            prevAng = prevAng + vel;
        } //simplified
    }

    void FixedUpdate()
    {
        joint.angularVelocity = Vector3.zero;
    }

    void LateUpdate()
    {
        if (changed == true) {
            if ((motorTag.Substring(2, 1) == "p" || motorTag.Substring(2, 1) == "P") && angle == prevAng)
            {
                transform.localRotation = Quaternion.Euler(new Vector3(angle, 0, 0));
            }

            else if ((motorTag.Substring(2, 1) == "r" || motorTag.Substring(2, 1) == "R") && angle == prevAng)
            {
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }

            else if ((motorTag.Substring(2, 1) == "y" || motorTag.Substring(2, 1) == "Y") && angle == prevAng)
            {
                transform.localRotation = Quaternion.Euler(new Vector3(0, angle, 0));
            }
            else { }
            changed = false;
        }
    }
}