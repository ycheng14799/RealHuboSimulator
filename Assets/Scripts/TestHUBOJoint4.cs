using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestHUBOJoint4 : MonoBehaviour
{

    public int direction = 1;
    public string motorTag;
    public string txtFile = "motorLocsSpeeds";

    private float angle;
    private float prevAng;
    private int textInd;
    private string txtIn;
    private Rigidbody joint;
    private ConfigurableJoint configJoint;

    private int velInd;
    private int posInd;
    private float vel;

    private string velString;
    private string posString;
    private Vector3 axes;
    private Vector3 origPos;
    private Vector3 rotation;
    private bool posLock;


    void Start()
    {
        origPos = gameObject.transform.localPosition;
        TextAsset txtAssets = (TextAsset)Resources.Load(txtFile);
        txtIn = txtAssets.text;
        joint = GetComponent<Rigidbody>();
        motorTag = gameObject.name;
        motorTag = motorTag.Substring(5, motorTag.Length - 5);
        if (name != "Body_Torso" && gameObject.GetComponent<ConfigurableJoint>() != null)
        {
            configJoint = GetComponent<ConfigurableJoint>();
            axes = GetComponent<ConfigurableJoint>().axis;
        }
        else
        { }

        if (motorTag.Contains("RSR") || motorTag.Contains("KP"))
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
        gameObject.transform.localPosition = origPos;
        TextAsset txtAssets = (TextAsset)Resources.Load(txtFile);
        txtIn = txtAssets.text;

        textInd = txtIn.IndexOf(motorTag);
        if (textInd != -1)
        {

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



            if (angle > prevAng)
            {
                if (prevAng + vel > angle)
                {
                    vel = angle - prevAng;
                }
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
            }
            else
            {
                vel = 0;
            }
            if (angle != prevAng)
            {
                rotation = axes * prevAng;
                posLock = true;
                //Quaternion rot = Quaternion.AngleAxis(vel, axes);
                transform.localRotation = Quaternion.Euler(rotation);
            }

            //if (configJoint.axis.x != 0)
            //{
            //    joint.constraints = RigidbodyConstraints.None;
            //    joint.constraints = RigidbodyConstraints.FreezeRotationX;
            //}
            //else if (configJoint.axis.y != 0)
            //{
            //    joint.constraints = RigidbodyConstraints.None;
            //    joint.constraints = RigidbodyConstraints.FreezeRotationY;
            //}
            //else if (configJoint.axis.z != 0)
            //{
            //    joint.constraints = RigidbodyConstraints.None;
            //    joint.constraints = RigidbodyConstraints.FreezeRotationZ;
            //}
            prevAng = prevAng + vel;
        }
        else
        {
            if (name != "Body_Torso" && gameObject.GetComponent<ConfigurableJoint>() != null)
            {
                if (configJoint.axis.x != 0)
                {
                    joint.constraints = RigidbodyConstraints.None;
                    joint.constraints = RigidbodyConstraints.FreezeRotationX;
                }
                else if (configJoint.axis.y != 0)
                {
                    joint.constraints = RigidbodyConstraints.None;
                    joint.constraints = RigidbodyConstraints.FreezeRotationY;
                }
                else if (configJoint.axis.z != 0)
                {
                    joint.constraints = RigidbodyConstraints.None;
                    joint.constraints = RigidbodyConstraints.FreezeRotationZ;
                }
            }
        }
        if (name != "Body_Torso")
        {
            if (angle == prevAng /*&& posLock == true*/)
            {
                joint.constraints = RigidbodyConstraints.FreezeRotation;
                //transform.localRotation = Quaternion.Euler(axes * angle);
                //posLock = false;
            }
        }
    }
}

