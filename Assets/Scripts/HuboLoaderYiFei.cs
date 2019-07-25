//index//
//collider section
//addjoint


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class HuboLoaderYiFei : MonoBehaviour
{
    /*
    public GameObject[] childObjects1;
    public GameObject[] childObjects2;

    public string bodyTag;
    public string transform1;
    public string parToCur;
    public string baseToMass;
    public string bodyName;
    public string hullName;
    public string bodyTranslate;
    public string col1;
    public string col2;

    public float traX;
    public float traY;
    public float traZ;

    public float anchX;
    public float anchY;
    public float anchZ;

    public int axX;
    public int axY;
    public int axZ;
    public int placeholder;

    public float limMin;
    public float limMax;

    public float massVal;
    public Vector3 massPos;

    public string traXstr;
    public string traYstr;
    public string traZstr;

    public int space1Count;
    public int space2Count;
    public int perCount;

    public string parentName;
    public float mass;


    public GameObject colObj1;
    public GameObject colObj2;

    public Vector3 pos;
    public Vector3 anch;
    public ConfigurableJoint curJoint;
    public Mesh curHullMesh;
    public MeshCollider curBodyMeshCol;
    public GameObject curObj;
    public GameObject curHull;
    public GameObject newBod;
    public Transform curObjTrans;
    private string path = "Assets/Resources/motorLocsSpeeds.txt";
    */

    // Function for parsing the rotation axis 
    // Indices:
    // 0, 1, 2: rotation axis 
    // 3: angle of rotation
    private float[] parseRotationAxis(string rotationAxisString)
    {
        rotationAxisString = rotationAxisString.Trim();
        float[] rotationInfo = new float[4];
        int firstSpaceIdx = rotationAxisString.IndexOf(" ");
        int secondSpaceIdx = rotationAxisString.IndexOf(" ", firstSpaceIdx + 1);
        int thirdSpaceIdx = rotationAxisString.IndexOf(" ", secondSpaceIdx + 1);
        rotationInfo[0] = float.Parse(rotationAxisString.Substring(0, firstSpaceIdx));
        rotationInfo[1] = float.Parse(rotationAxisString.Substring(firstSpaceIdx + 1, secondSpaceIdx - (firstSpaceIdx + 1)));
        rotationInfo[2] = float.Parse(rotationAxisString.Substring(secondSpaceIdx + 1, thirdSpaceIdx - (secondSpaceIdx + 1)));
        rotationInfo[3] = float.Parse(rotationAxisString.Substring(thirdSpaceIdx + 1, rotationAxisString.Length - (thirdSpaceIdx + 1)));
        return rotationInfo;
    }

    // Function for parsing translation 
    // Indices:
    // 0, 1, 2: x, y, z of translation 
    private Vector3 parseVector(string vectorString)
    {
        vectorString = vectorString.Trim();
        Vector3 returnVec = new Vector3();
        int firstSpaceIdx = vectorString.IndexOf(" ");
        int secondSpaceIdx = vectorString.IndexOf(" ", firstSpaceIdx + 1);
        returnVec.x = float.Parse(vectorString.Substring(0, firstSpaceIdx));
        returnVec.y = float.Parse(vectorString.Substring(firstSpaceIdx + 1, secondSpaceIdx - (firstSpaceIdx + 1)));
        returnVec.z = float.Parse(vectorString.Substring(secondSpaceIdx + 1, vectorString.Length - (secondSpaceIdx + 1)));
        return returnVec;
    }

    void Start()
    {
        // Read in XML file containing assembly specifications 
        TextAsset textAsset = (TextAsset)Resources.Load("huboplus.kinbody");
        XmlDocument document = new XmlDocument();
        document.LoadXml(textAsset.text);
        XmlNode kinbody = document.SelectSingleNode("kinbody");

        // Part specifications 
        XmlNodeList bodies = kinbody.SelectNodes("body");
        
        // Place Hubo parts in scene 
        foreach(XmlNode currPar in bodies)
        {
            // GameObject container for part
            string currParName = currPar.Attributes["name"].Value;
            GameObject currParObj = new GameObject(currParName);

            // Part mesh set-up
            XmlNodeList currParGeoms = currPar.SelectNodes("geom");
            foreach(XmlNode currParGeom in currParGeoms)
            {
                // Part mesh
                XmlNode render = currParGeom.SelectSingleNode("render");
                // Collision hull
                XmlNode colHull = currParGeom.SelectSingleNode("data");

                // Place mesh in scene 
                // triangle custom meshes 
                if (currParGeom.Attributes["type"].Value == "trimesh")
                {
                    // Mesh 
                    string meshName = render.InnerText;
                    int meshNamePerIdx = meshName.IndexOf(".");
                    meshName = meshName.Substring(0, meshNamePerIdx);
                    int meshFirstSpaceIdx = meshName.IndexOf("_");
                    int meshSecondSpaceIdx = meshName.Substring(meshFirstSpaceIdx + 1, meshName.Length - (meshFirstSpaceIdx + 1)).IndexOf("_");
                    string meshTag = meshName.Substring(0, meshFirstSpaceIdx + meshSecondSpaceIdx + 1);
                    GameObject meshObj = Instantiate(Resources.Load(meshName)) as GameObject;
                    meshObj.name = meshName;
                    meshObj.tag = meshTag;
                    meshObj.transform.parent = currParObj.transform;
                    // Collision hull
                    string hullName = colHull.InnerText;
                    int hullNamePerIdx = hullName.IndexOf(".");
                    hullName = hullName.Substring(0, hullNamePerIdx);
                    GameObject hull = Resources.Load(hullName) as GameObject;
                    Mesh hullMesh = hull.transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh;
                    meshObj.AddComponent<MeshCollider>();
                    meshObj.GetComponent<MeshCollider>().sharedMesh = hullMesh;
                    meshObj.GetComponent<MeshCollider>().convex = true;
                }
                // cylinders 
                if (currParGeom.Attributes["type"].Value == "cylinder")
                {
                    GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    cylinder.transform.parent = currParObj.transform;
                    // Set radius and height
                    float rad = float.Parse(currParGeom.SelectSingleNode("radius").InnerText);
                    float height = float.Parse(currParGeom.SelectSingleNode("height").InnerText);
                    cylinder.transform.localScale = new Vector3(rad, height, rad);
                    // Set rotation
                    float[] rotationInfo = this.parseRotationAxis(currParGeom.SelectSingleNode("rotationaxis").InnerText);
                    cylinder.transform.localRotation = Quaternion.AngleAxis(rotationInfo[3],
                        new Vector3(rotationInfo[0], rotationInfo[1], rotationInfo[2]));
                    // Set translation
                    cylinder.transform.localPosition = this.parseVector(currParGeom.SelectSingleNode("translation").InnerText);
                }
                // boxes 
                if (currParGeom.Attributes["type"].Value == "box")
                {
                    GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    box.transform.parent = currParObj.transform;
                    // Set scale
                    box.transform.localScale = this.parseVector(currParGeom.SelectSingleNode("extents").InnerText);
                    // Set translation 
                    if (currParGeom.SelectSingleNode("translation")!= null)
                    {
                        box.transform.localPosition = this.parseVector(currParGeom.SelectSingleNode("translation").InnerText);
                    }
                }
            }
        }

        // Set up hierarchy & assemble 
        foreach(XmlNode currPar in bodies)
        {
            // Get name of current part 
            string currParName = currPar.Attributes["name"].Value;

            // The Body_Torso part is designated as the root 
            // All other parts will be a child of Body_Torso 
            if(currParName != "Body_Torso")
            {
                // Get part 
                GameObject currParObj = GameObject.Find(currParName);
                // Get part parent 
                string currParParentName = currPar.SelectSingleNode("offsetfrom").InnerText;
                GameObject currParParent = GameObject.Find(currParParentName);
                // Set parent
                currParObj.transform.parent = currParParent.transform;

                // Apply local translation 
                currParObj.transform.localPosition = this.parseVector(currPar.SelectSingleNode("translation").InnerText); 

            }
        }
        
        /*
        // Assign phyical properties to rigidbodies 
        for (int n = 0; n < bodies.Count; n++)
        {
            {
                XmlNode curPar = bodies.Item(n);

                bodyName = curPar.Attributes["name"].Value;

                curObj = GameObject.Find(bodyName);

                curObj.AddComponent<Rigidbody>().useGravity = false;

                XmlNode mass = curPar.SelectSingleNode("mass");
                XmlNode totalMass = mass.SelectSingleNode("total");
                XmlNode com = mass.SelectSingleNode("com");

                massVal = float.Parse(totalMass.InnerText);


                if(com != null)
                {
                    string axisStr = com.InnerText; //parent to current
                    space1Count = axisStr.IndexOf(" ");
                    traXstr = axisStr.Substring(0, space1Count);
                    space2Count = axisStr.Substring(space1Count + 1, axisStr.Length - (space1Count + 1)).IndexOf(" ");
                    traYstr = axisStr.Substring(space1Count + 1, space2Count);
                    space2Count = space1Count + space2Count;
                    traZstr = axisStr.Substring(space2Count + 1, axisStr.Length - (space2Count + 1));


                    massPos.x = float.Parse(traXstr);
                    massPos.y = float.Parse(traYstr);
                    massPos.z = float.Parse(traZstr);
                }

                curObj.GetComponent<Rigidbody>().mass = massVal;
                curObj.GetComponent<Rigidbody>().centerOfMass = massPos;



            }
        }
        
        //joint stuff
        XmlNodeList joints = kinbody.SelectNodes("Joint");
        for (int o = 0; o < joints.Count; o++)
        {
            XmlNodeList joinBods = joints.Item(o).SelectNodes("body");

            string jointBody1 = joinBods.Item(0).InnerText;
            string jointBody2 = joinBods.Item(1).InnerText;
            //print(jointBody1);
            //print(jointBody2);

            

            GameObject child = GameObject.Find(jointBody2);
            GameObject parent = GameObject.Find(jointBody1);

            {
                if (joints.Item(o).SelectSingleNode("anchor") != null)
                {
                    string anchorStr = joints.Item(o).SelectSingleNode("anchor").InnerText; //parent to current
                    space1Count = anchorStr.IndexOf(" ");
                    traXstr = anchorStr.Substring(0, space1Count);
                    space2Count = anchorStr.Substring(space1Count + 1, anchorStr.Length - (space1Count + 1)).IndexOf(" ");
                    traYstr = anchorStr.Substring(space1Count + 1, space2Count);
                    space2Count = space1Count + space2Count;
                    traZstr = anchorStr.Substring(space2Count + 1, anchorStr.Length - (space2Count + 1));

                    anchX = float.Parse(traXstr);
                    anchY = float.Parse(traYstr);
                    anchZ = float.Parse(traZstr);
                }
            }
            if (joints.Item(o).SelectSingleNode("limitsdeg") != null)
            {
                string limitsStr = joints.Item(o).SelectSingleNode("limitsdeg").InnerText; //parent to current
                space1Count = limitsStr.IndexOf(" ");
                traXstr = limitsStr.Substring(0, space1Count);
                traYstr = limitsStr.Substring(space1Count + 1, limitsStr.Length - (space1Count + 1));

                limMin = float.Parse(traXstr);
                limMax = float.Parse(traYstr);
                //print("Low");
                //print(limMin);
                //print("High");
                //print(limMax);
            }
            if (joints.Item(o).SelectSingleNode("axis") != null)
            {
                string axisStr = joints.Item(o).SelectSingleNode("axis").InnerText; //parent to current
                space1Count = axisStr.IndexOf(" ");
                traXstr = axisStr.Substring(0, space1Count);
                space2Count = axisStr.Substring(space1Count + 1, axisStr.Length - (space1Count + 1)).IndexOf(" ");
                traYstr = axisStr.Substring(space1Count + 1, space2Count);
                space2Count = space1Count + space2Count;
                traZstr = axisStr.Substring(space2Count + 1, axisStr.Length - (space2Count + 1));


                axX = Mathf.Abs(int.Parse(traXstr));
                axY = Mathf.Abs(int.Parse(traYstr));
                axZ = Mathf.Abs(int.Parse(traZstr));
            }

            ////////////////////////////////////////
            //addjoint//
            child.AddComponent<ConfigurableJoint>();
            curJoint = child.GetComponent<ConfigurableJoint>();
            anch = new Vector3(anchX, anchY, anchZ);
            curJoint.anchor = anch;
            //curJoint.useLimits = true;
            curJoint.xMotion = ConfigurableJointMotion.Locked;
            curJoint.yMotion = ConfigurableJointMotion.Locked;
            curJoint.zMotion = ConfigurableJointMotion.Locked;

            curJoint.angularYMotion = ConfigurableJointMotion.Locked;
            curJoint.angularZMotion = ConfigurableJointMotion.Locked;

            SoftJointLimit limMinSoft = curJoint.lowAngularXLimit;
            limMinSoft.limit = limMin;
            curJoint.lowAngularXLimit = limMinSoft;

            SoftJointLimit limMaxSoft = curJoint.highAngularXLimit;
            limMaxSoft.limit = limMax;
            curJoint.lowAngularXLimit = limMaxSoft;
            
            {
            */
        /*
        if (axX != 0)
        {
            //print("ping");
            curJoint.angularYMotion = ConfigurableJointMotion.Locked;
            curJoint.angularZMotion = ConfigurableJointMotion.Locked;
            //child.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;
            //child.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
        }
        else if (axY != 0)
        {
            //print("pang");
            curJoint.angularXMotion = ConfigurableJointMotion.Locked;
            curJoint.angularZMotion = ConfigurableJointMotion.Locked;
            //child.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
            //child.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
        }

        else if (axZ != 0)
        {
            //print("pong");
            curJoint.angularYMotion = ConfigurableJointMotion.Locked;
            curJoint.angularXMotion = ConfigurableJointMotion.Locked;
            //child.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;
            //child.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
        }
        */
        /*
    }

    curJoint.connectedBody = parent.GetComponent<Rigidbody>();
    curJoint.axis = new Vector3(axX, axY, axZ); /////set AXIS
    //print(curJoint);



    //dont forget to set axis and fix limits

}



/*
for (int n = 0; n < bodies.Count; n++)
{
    {
        XmlNode curPar = bodies.Item(n);

        bodyName = curPar.Attributes["name"].Value;

        curObj = GameObject.Find(bodyName);
        curObj.GetComponent<Rigidbody>().useGravity = true;

        if (bodyName != "Body_Torso")
        {
            //curObj.AddComponent<TestHUBOJoint4>();
            curObj.AddComponent<PositionLock>();
        }




    }
}



for (int p = 0; p < collisionIgnores.Count; p++)
{
    string limitsStr = collisionIgnores.Item(p).InnerText; //parent to current
    space1Count = limitsStr.IndexOf(" ");
    col1 = limitsStr.Substring(0, space1Count);
    col2 = limitsStr.Substring(space1Count + 1, limitsStr.Length - (space1Count + 1));

    childObjects1 = GameObject.FindGameObjectsWithTag(col1);
    childObjects2 = GameObject.FindGameObjectsWithTag(col2);


    for (int q = 0; q < childObjects1.Length; q++)
    {
        colObj1 = childObjects1[q];
        for (int r = 0; r < childObjects2.Length; r++)
        {
            colObj2 = childObjects2[r];
            Physics.IgnoreCollision(colObj1.GetComponent<MeshCollider>(), colObj2.GetComponent<MeshCollider>(), true);
        }

    }
}





GameObject.Find("Body_Torso").transform.Rotate(new Vector3(-90, 0, 0));
GameObject.Find("Body_Torso").GetComponent<Rigidbody>().isKinematic = true;


*/


    }
    /*
    void Update()
    {

        if (Input.GetKey("escape"))
            Application.Quit();
    }
    */
}
//Use configurable joint limits to store limits in order to pass them to the joints script