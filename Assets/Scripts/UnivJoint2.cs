using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnivJoint2 : MonoBehaviour
{
    /*


    public int direction = 1;
    public string motorTag;
    public string txtFile = "motorLocsSpeeds";
    public string parentName;
    public GameObject parent;
    public Rigidbody parRig;

    private float angle;
    public float prevAng;
    private int textInd;
    private string txtIn;
    private Rigidbody jointRig;
    private ConfigurableJoint charJoint;

    private int velInd;
    private int angleInd;
    private float vel;

    private string velString;
    private string posString;

    // Use this for initialization
    void Start()
    {
        TextAsset txtAssets = (TextAsset)Resources.Load(txtFile);
        txtIn = txtAssets.text;
        jointRig = GetComponent<Rigidbody>();
        parRig = parent.GetComponent<Rigidbody>();
        charJoint = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        {
            TextAsset txtAssets = (TextAsset)Resources.Load(txtFile);
            txtIn = txtAssets.text;

            textInd = txtIn.IndexOf(motorTag);

            velInd = textInd + motorTag.Length + 1;
            angleInd = textInd + motorTag.Length + 5;

            velString = txtIn.Substring(velInd, 4); //should be 4 chars including .
            posString = txtIn.Substring(angleInd, 4); //must be 4 chars with the first a 0 or negative sign and between -180 and 180

            vel = float.Parse(velString);
            angle = System.Int32.Parse(posString) * direction;


            vel = vel * Mathf.Rad2Deg / 60;
        }
        
        


        if (motorTag.Substring(2, 1) == "p" || motorTag.Substring(2, 1) == "P")
        {
            charJoint.targetRotation = Quaternion.Euler(new Vector3(angle, 0, 0));
            JointDrive drive = new JointDrive();
            drive.positionSpring = 150;

            drive.mode = JointDriveMode.Position;
            drive.maximumForce = 4;

            charJoint.angularXDrive = drive;
        }
    }*/
}

    //void LateUpdate()
    //{
    //    joint.constraints = RigidbodyConstraints.FreezeRotationX;
    //}
//}

//check for framerate