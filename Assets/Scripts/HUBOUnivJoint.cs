using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUBOUnivJoint : MonoBehaviour
{
    
    public int direction = 1;
    public string motorTag;
    public string txtFile = "motorLocsSpeeds";

    private float angle;
    private float prevAng;
    private int textInd;
    private string txtIn;
    private Rigidbody joint;

    private int velInd;
    private int posInd;
    private float vel;
    private Vector3 rotation;

    private string velString;
    private string posString;
    
    
    void Start()
    {
        TextAsset txtAssets = (TextAsset)Resources.Load(txtFile);
        txtIn = txtAssets.text;
        joint = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        {
            TextAsset txtAssets = (TextAsset)Resources.Load(txtFile);
            txtIn = txtAssets.text;

            textInd = txtIn.IndexOf(motorTag);

            velInd = textInd + motorTag.Length + 1;
            posInd = textInd + motorTag.Length + 5;

            velString = txtIn.Substring(velInd, 4); //should be 4 chars including .
            posString = txtIn.Substring(posInd, 4); //must be 4 chars with the first a 0 or negative sign and between -180 and 180

            vel = float.Parse(velString);
            angle = float.Parse(posString) * direction;


            vel = vel * Mathf.Rad2Deg * Time.deltaTime;
        }

        {
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

            if (motorTag.Substring(2, 1) == "p" || motorTag.Substring(2, 1) == "P")
            {
                transform.Rotate(new Vector3(vel, 0, 0));
                joint.constraints = RigidbodyConstraints.None;
                joint.constraints = RigidbodyConstraints.FreezeRotationX;
            }
            if (motorTag.Substring(2, 1) == "y" || motorTag.Substring(2, 1) == "Y")
            {
                transform.Rotate(new Vector3(0, vel, 0));
                joint.constraints = RigidbodyConstraints.None;
                joint.constraints = RigidbodyConstraints.FreezeRotationY;
            }
            if (motorTag.Substring(2, 1) == "r" || motorTag.Substring(2, 1) == "R")
            {
                transform.Rotate(new Vector3(0, 0, vel));
                joint.constraints = RigidbodyConstraints.None;
                joint.constraints = RigidbodyConstraints.FreezeRotationZ;
            }

            prevAng = prevAng + vel;
        } //simplified
    }

        void LateUpdate()
        {
            if ((motorTag.Substring(2, 1) == "p" || motorTag.Substring(2, 1) == "P") && angle == prevAng)
            {
                transform.localRotation = Quaternion.Euler(new Vector3(angle, 0, 0));
            }

            if ((motorTag.Substring(2, 1) == "r" || motorTag.Substring(2, 1) == "R") && angle == prevAng)
            {
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }

            if ((motorTag.Substring(2, 1) == "y" || motorTag.Substring(2, 1) == "Y") && angle == prevAng)
            {
                transform.localRotation = Quaternion.Euler(new Vector3(0, angle, 0));
            }
        }
}


                /*

            if ((motorTag.Substring(2, 1) == "p" || motorTag.Substring(2, 1) == "P"))
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
                joint.constraints = RigidbodyConstraints.None;
                joint.constraints = RigidbodyConstraints.FreezeRotationX;
            }

            else if ((motorTag.Substring(2, 1) == "y" || motorTag.Substring(2, 1) == "Y"))
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
                joint.constraints = RigidbodyConstraints.None;
                joint.constraints = RigidbodyConstraints.FreezeRotationY;
            }

            else if ((motorTag.Substring(2, 1) == "r" || motorTag.Substring(2, 1) == "R"))
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
                joint.constraints = RigidbodyConstraints.None;
                joint.constraints = RigidbodyConstraints.FreezeRotationZ;
            }

        */
        //definitely works