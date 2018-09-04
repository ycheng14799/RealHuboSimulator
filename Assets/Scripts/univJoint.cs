using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class univJoint : MonoBehaviour
{
    /*public int direction = 1;
    public string motorTag;
    public string txtFile = "motorLocsSpeeds";
    public string parentName;

    private float altField1;
    private float altField2;
    private float altField3;
    private float angle;
    public float prevAng;
    private int textInd;
    private string txtIn;
    private Rigidbody joint;
    private Transform parent;
    private Transform child;
    private bool locked;

    private int velInd;
    private int posInd;
    private float vel;

    private string velString;
    private string posString;
    */
    // Use this for initialization
    void Start()
    {/*
        TextAsset txtAssets = (TextAsset)Resources.Load(txtFile);
        txtIn = txtAssets.text;
        joint = GetComponent<Rigidbody>();
        parent = GameObject.FindWithTag(parentName).GetComponent<Transform>();
        child = GetComponent<Transform>();*/
    }

    // Update is called once per frame
    /*void Update()
    {
        TextAsset txtAssets = (TextAsset)Resources.Load(txtFile);
        txtIn = txtAssets.text;

        textInd = txtIn.IndexOf(motorTag);

        velInd = textInd + motorTag.Length + 1;
        posInd = textInd + motorTag.Length + 5;

        velString = txtIn.Substring(velInd,4); //should be 4 chars including .
        posString = txtIn.Substring(posInd,4); //must be 4 chars with the first a 0 or negative sign and between -180 and 180

        vel = float.Parse(velString);
        angle = System.Int32.Parse(posString)*direction;


        vel = vel*Mathf.Rad2Deg / 60;

        if (motorTag.Substring(2, 1) == "p" || motorTag.Substring(2, 1) == "P")
        {
            if (angle > prevAng)
            {
                if (prevAng != angle)
                {
                    if ((prevAng + vel) > angle)
                    {
                        vel = angle - prevAng;
                        transform.Rotate(new Vector3(vel, 0, 0));//use middle letter, (roll,pitch,yaw)
                        prevAng = prevAng + vel;
                    }
                    else
                    {
                        transform.Rotate(new Vector3(vel, 0, 0));//use middle letter, (roll,pitch,yaw)
                        prevAng = prevAng + vel;
                    }
                }
                //joint.constraints = RigidbodyConstraints.None;
                //joint.constraints = RigidbodyConstraints.FreezeRotationX;
            }

            else if (angle < prevAng)
            {
                if (prevAng != angle)
                {
                    if ((prevAng - vel) < angle)
                    {
                        vel = Mathf.Abs(angle - prevAng);
                        transform.Rotate(new Vector3(-vel, 0, 0));//use middle letter, (roll,pitch,yaw)
                        prevAng = prevAng - vel;
                    }
                    else
                    {
                        transform.Rotate(new Vector3(-vel, 0, 0));//use middle letter, (roll,pitch,yaw)
                        prevAng = prevAng - vel;
                    }
                }
                //joint.constraints = RigidbodyConstraints.None;
                //joint.constraints = RigidbodyConstraints.FreezeRotationX;
            }
            else
            {
                //joint.constraints = RigidbodyConstraints.None;
                //joint.constraints = RigidbodyConstraints.FreezeRotationX;
            }
        }



        else if (motorTag.Substring(2, 1) == "y" || motorTag.Substring(2, 1) == "Y")
        {
            if (angle > prevAng)
            {
                if (prevAng != angle)
                {
                    if ((prevAng + vel) > angle)
                    {
                        vel = angle - prevAng;
                        transform.Rotate(new Vector3(0, vel, 0));//use middle letter, (roll,pitch,yaw)
                        prevAng = prevAng + vel;
                    }
                    else
                    {
                        transform.Rotate(new Vector3(0, vel, 0));//use middle letter, (roll,pitch,yaw)
                        prevAng = prevAng + vel;
                    }
                }

                joint.constraints = RigidbodyConstraints.FreezeRotationY;
            }

            else if (angle < prevAng)
            {
                if (prevAng != angle)
                {
                    if ((prevAng - vel) < angle)
                    {
                        vel = Mathf.Abs(angle - prevAng);
                        transform.Rotate(new Vector3(0, -vel, 0));//use middle letter, (roll,pitch,yaw)
                        prevAng = prevAng - vel;
                    }
                    else
                    {
                        transform.Rotate(new Vector3(0, -vel, 0));//use middle letter, (roll,pitch,yaw)
                        prevAng = prevAng - vel;
                    }
                }

                joint.constraints = RigidbodyConstraints.FreezeRotationY;
            }
        }



        else if ((motorTag.Substring(2, 1) == "r" || motorTag.Substring(2, 1) == "R"))
        {
            if (angle > prevAng)
            {
                if (prevAng != angle)
                {
                    if ((prevAng + vel) > angle)
                    {
                        vel = angle - prevAng;
                        transform.Rotate(new Vector3(0, 0, vel));//use middle letter, (roll,pitch,yaw)
                        prevAng = prevAng + vel;
                    }
                    else
                    {
                        transform.Rotate(new Vector3(0, 0, vel));//use middle letter, (roll,pitch,yaw)
                        prevAng = prevAng + vel;
                    }
                }

                joint.constraints = RigidbodyConstraints.FreezeRotationZ;
            }

            else if (angle < prevAng)
            {
                if (prevAng != angle)
                {
                    if ((prevAng - vel) < angle)
                    {
                        vel = Mathf.Abs(angle - prevAng);
                        transform.Rotate(new Vector3(0, 0, -vel));//use middle letter, (roll,pitch,yaw)
                        prevAng = prevAng - vel;
                    }
                    else
                    {
                        transform.Rotate(new Vector3(0, 0, -vel));//use middle letter, (roll,pitch,yaw)
                        prevAng = prevAng - vel;
                    }
                }

                joint.constraints = RigidbodyConstraints.FreezeRotationZ;
            }
        }

        //joint.angularVelocity = Vector3.zero;////////
        //joint.velocity = Vector3.zero;////////
    }

    void FixedUpdate()
    {
        if (motorTag.Substring(2, 1) == "p" || motorTag.Substring(2, 1) == "P")
        {
            //joint.constraints = RigidbodyConstraints.None;
            //joint.constraints = RigidbodyConstraints.FreezeRotationX;
        }
    }

    void LateUpdate()
    {
        if ((motorTag.Substring(2, 1) == "p" || motorTag.Substring(2, 1) == "P") && angle == prevAng)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(angle, 0, 0));
        }
    }
    */

}

//check for framerate


/*        if (motorTag.Substring(2, 1) == "p" || motorTag.Substring(2, 1) == "P")
    {

        if (angle > prevAng)
        {
            if (prevAng != angle)
            {
                if ((prevAng + vel) > angle)
                {
                    vel = angle - prevAng;
                    child.Rotate(new Vector3(vel, 0, 0));//use middle letter, (roll,pitch,yaw)//use middle letter, (roll,pitch,yaw)
                    //prevAng = prevAng + vel;
                    prevAng = transform.localEulerAngles.x;
                }
                else
                {
                    child.Rotate(new Vector3(vel, 0, 0));//use middle letter, (roll,pitch,yaw)//use middle letter, (roll,pitch,yaw)
                    //prevAng = prevAng + vel;
                    prevAng = transform.localEulerAngles.x;
                }
            }
            joint.constraints = RigidbodyConstraints.FreezeRotationX;
            //joint.angularVelocity = Vector3.zero;////////
            //joint.velocity = Vector3.zero;////////
        }

        else if (angle < prevAng)
        {
            if (prevAng != angle)
            {
                if ((prevAng - vel) < angle)
                {
                    vel = Mathf.Abs(angle - prevAng);
                    child.Rotate(new Vector3(-vel, 0, 0));//use middle letter, (roll,pitch,yaw)
                    //prevAng = prevAng - vel;
                    prevAng = transform.localEulerAngles.x;
                }
                else
                {
                    child.Rotate(new Vector3(-vel, 0, 0));//use middle letter, (roll,pitch,yaw)
                    //prevAng = prevAng - vel;
                    prevAng = transform.localEulerAngles.x;
                }
            }

            joint.constraints = RigidbodyConstraints.FreezeRotationX;
            //joint.angularVelocity = Vector3.zero;////////
            //joint.velocity = Vector3.zero;////////
        }
        else
        {
            /*altField1 = Quaternion.ToEulerAngles(parent.rotation).x * Mathf.Rad2Deg;
            altField2 = Quaternion.ToEulerAngles(parent.rotation).y * Mathf.Rad2Deg;
            altField3 = Quaternion.ToEulerAngles(parent.rotation).z * Mathf.Rad2Deg;
            //transform.localRotation = Quaternion.AngleAxis(angle, transform.);
            transform.localRotation = Quaternion.Euler(new Vector3(angle+altField1, altField2, altField3));//retrieve axis and  
            joint.constraints = RigidbodyConstraints.FreezeRotationX;*/
//joint.angularVelocity = Vector3.zero; //////// (Still broken, need to remove forces somehow)
//joint.velocity = Vector3.zero; ////////
//       }
//     }*/


            /*
            if (motorTag.Substring(2, 1) == "p" || motorTag.Substring(2, 1) == "P")
            {
                if (angle > prevAng)
                {
                    if ((prevAng + vel) > angle)
                    {
                        vel = angle - prevAng;
                        transform.Rotate(new Vector3(vel, 0, 0));//use middle letter, (roll,pitch,yaw)
                        prevAng = prevAng + vel;
                    }
                    else
                    {
                        transform.Rotate(new Vector3(vel, 0, 0));//use middle letter, (roll,pitch,yaw)
                        prevAng = prevAng + vel;
                    }
                }

                else if (angle < prevAng)
                {
                    if ((prevAng - vel) < angle)
                    {
                        vel = Mathf.Abs(angle - prevAng);
                        transform.Rotate(new Vector3(-vel, 0, 0));//use middle letter, (roll,pitch,yaw)
                        prevAng = prevAng - vel; //to shorten prevang
                    }
                    else
                    {
                        transform.Rotate(new Vector3(-vel, 0, 0));//use middle letter, (roll,pitch,yaw)
                        prevAng = prevAng - vel;
                    }
                }
            }

            if (motorTag.Substring(2, 1) == "y" || motorTag.Substring(2, 1) == "Y")
            {
                if (angle > prevAng)
                {
                    if ((prevAng + vel) > angle)
                    {
                        vel = angle - prevAng;
                        transform.Rotate(new Vector3(0, vel, 0));//use middle letter, (roll,pitch,yaw)
                        prevAng = prevAng + vel;
                    }
                    else
                    {
                        transform.Rotate(new Vector3(0, vel, 0));//use middle letter, (roll,pitch,yaw)
                        prevAng = prevAng + vel;
                    }
                }

                else if (angle < prevAng)
                {
                    if ((prevAng - vel) < angle)
                    {
                        vel = Mathf.Abs(angle - prevAng);
                        transform.Rotate(new Vector3(0, -vel, 0));//use middle letter, (roll,pitch,yaw)
                        prevAng = prevAng - vel;
                    }
                    else
                    {
                        transform.Rotate(new Vector3(0, -vel, 0));//use middle letter, (roll,pitch,yaw)
                        prevAng = prevAng - vel;
                    }
                }
            }

            if (motorTag.Substring(2, 1) == "r" || motorTag.Substring(2, 1) == "R")
            {
                if (angle > prevAng)
                {
                    if ((prevAng + vel) > angle)
                    {
                        vel = angle - prevAng;
                        transform.Rotate(new Vector3(0, 0, vel));//use middle letter, (roll,pitch,yaw)
                        prevAng = prevAng + vel;
                    }
                    else
                    {
                        transform.Rotate(new Vector3(0, 0, vel));//use middle letter, (roll,pitch,yaw)
                        prevAng = prevAng + vel;
                    }
                }

                else if (angle < prevAng)
                {
                    if ((prevAng - vel) < angle)
                    {
                        vel = Mathf.Abs(angle - prevAng);
                        transform.Rotate(new Vector3(0, 0, -vel));//use middle letter, (roll,pitch,yaw)
                        prevAng = prevAng - vel;
                    }
                    else
                    {
                        transform.Rotate(new Vector3(0, 0, -vel));//use middle letter, (roll,pitch,yaw)
                        prevAng = prevAng - vel;
                    }
                }
            }
        *///simplify stuff