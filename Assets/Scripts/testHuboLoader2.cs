//index//
//collider section
//addjoint


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class testHuboLoader2 : MonoBehaviour
{

    //public List<testPart> parts = new List<testPart>();
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
    //private Vector3 rotCor = new Vector3(-90, 0, 0);
    public ConfigurableJoint curJoint;
    public Mesh curHullMesh;
    public MeshCollider curBodyMeshCol;
    public GameObject curObj;
    public GameObject curHull;
    public GameObject newBod;
    public Transform curObjTrans;
    private string path = "Assets/Resources/motorLocsSpeeds.txt";
    //private string InitSet = "LSP 1.0 0000 LSR 1.0 0000 LSY 1.0 0000 LEP 1.0 0000 LWY 1.0 0000 LWP 1.0 0000 LHY 1.0 0000 LHR 1.0 0000 LHP 1.0 0000 LKP 1.0 0000 LAP 1.0 0000 LAR 1.0 0000 RSP 1.0 0000 RSR 1.0 0000 RSY 1.0 0000 REP 1.0 0000 RWY 1.0 0000 RWP 1.0 0000 RHY 1.0 0000 RHR 1.0 0000 RHP 1.0 0000 RKP 1.0 0000 RAP 1.0 0000 RAR 1.0 0000 NK1 1.0 0000 NK2 1.0 0000 Hip 1.0 0000";
    //public Transform curHullTrans;

    void Start()
    {
        //using (StreamWriter writer = new StreamWriter(path))
        //{
        //    writer.Write(InitSet);
        //    writer.Close();
        //}

        TextAsset textAsset = (TextAsset)Resources.Load("huboplus.kinbody");
        XmlDocument document = new XmlDocument();
        document.LoadXml(textAsset.text);

        //XmlDocument document = new XmlDocument();
        //document.Load("Assets/Resources/huboplus.kinbody.xml");

        //XmlNodeList tstprts = document.GetElementsByTagName("testPart"); //go down from kinbody?
        XmlNode kinbody = document.SelectSingleNode("kinbody");
        //XmlNode testParts = kinbody.SelectSingleNode("testParts");
        XmlNodeList bodies = kinbody.SelectNodes("body");
        XmlNodeList collisionIgnores = kinbody.SelectNodes("adjacent");

        //print(bodies.Count);

        for (int i = 0; i < bodies.Count; i++)
        {
            XmlNode curPar = bodies.Item(i);
            XmlNodeList geoms = curPar.SelectNodes("geom");

            Instantiate(GameObject.Find("blank"));

            newBod = GameObject.Find("blank(Clone)");
            newBod.name = curPar.Attributes["name"].Value;

            for (int j = 0; j < geoms.Count; j++)
            {
                XmlNode curGeom = geoms.Item(j);
                if (curGeom.Attributes["type"].Value == "trimesh")
                {
                    XmlNode render = curGeom.SelectSingleNode("render");
                    XmlNode data = curGeom.SelectSingleNode("data"); //hull
                    XmlNode transl = curGeom.SelectSingleNode("translation");
                    bodyName = render.InnerText;
                    hullName = data.InnerText;
                    perCount = bodyName.IndexOf(".");
                    bodyName = bodyName.Substring(0, perCount);

                    perCount = hullName.IndexOf(".");
                    hullName = hullName.Substring(0, perCount);

                    //bodyTranslate = transl.InnerText;
                    ////print(bodyName);
                    ////print(bodyTranslate);

                    //{
                    //    space1Count = bodyTranslate.IndexOf(" ");
                    //    traXstr = bodyTranslate.Substring(0, space1Count);
                    //    space2Count = bodyTranslate.Substring(space1Count + 1, bodyTranslate.Length - (space1Count + 1)).IndexOf(" ");
                    //    traYstr = bodyTranslate.Substring(space1Count + 1, space2Count);
                    //    space2Count = space1Count + space2Count;
                    //    traZstr = bodyTranslate.Substring(space2Count + 1, bodyTranslate.Length - (space2Count + 1));

                    //    traX = float.Parse(traXstr);
                    //    traY = float.Parse(traYstr);
                    //    traZ = float.Parse(traZstr);

                    curObj = Resources.Load(bodyName) as GameObject;

                    space1Count = bodyName.IndexOf("_");
                    space2Count = bodyName.Substring(space1Count + 1, bodyName.Length - (space1Count + 1)).IndexOf("_");
                    bodyTag = bodyName.Substring(0, space1Count + space2Count + 1);

                    curObj = Instantiate(curObj);
                    curObj.name = bodyName;
                    curObj.tag = bodyTag;
                    ////////////////////////////

                    curObjTrans = curObj.GetComponent<Transform>();

                    curObj.transform.SetParent(newBod.transform);

                    //curHull = GameObject.Find(hullName);
                    //curHullTrans = curHull.GetComponent<Transform>();


                    pos = new Vector3(traX, traY, traZ);

                    curObjTrans.position = pos;
                    //curHullTrans.position = pos;
                    //}

                    {//////////colliders section
                        curHull = Resources.Load(hullName) as GameObject;

                        curHullMesh = curHull.transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh;



                        curObj.AddComponent<MeshCollider>().inflateMesh = true;
                        //print(curHullMesh.name);
                        curObj.GetComponent<MeshCollider>().sharedMesh = curHullMesh;
                        curObj.GetComponent<MeshCollider>().convex = true;
                        curObj.GetComponent<MeshCollider>().skinWidth = 0.000000000000000000001F;


                    }
                }

                


            }



            //print(curPar.Attributes["name"].Value);
        }

        for (int k = 0; k < bodies.Count; k++)
        {
            XmlNode curPar = bodies.Item(k);

            bodyName = curPar.Attributes["name"].Value;

            curObj = GameObject.Find(bodyName);

            //newBod.AddComponent<Rigidbody>();

            if (curObj.name == "Body_Torso")
            {
                curObj.transform.position = new Vector3(0, 0, 0);
            }
            else
            {
                XmlNode offsetBody = curPar.SelectSingleNode("offsetfrom");
                parentName = offsetBody.InnerText;
                curObj.transform.SetParent(GameObject.Find(parentName).transform);

                //    XmlNodeList translations = curPar.SelectNodes("translation");

                //    {
                //        bodyTranslate = translations.Item(1).InnerText; //parent to current
                //        //print(bodyTranslate);
                //        space1Count = bodyTranslate.IndexOf(" ");
                //        traXstr = bodyTranslate.Substring(0, space1Count);
                //        space2Count = bodyTranslate.Substring(space1Count + 1, bodyTranslate.Length - (space1Count + 1)).IndexOf(" ");
                //        traYstr = bodyTranslate.Substring(space1Count + 1, space2Count);
                //        space2Count = space1Count + space2Count;
                //        traZstr = bodyTranslate.Substring(space2Count + 1, bodyTranslate.Length - (space2Count + 1));

                //        traX = float.Parse(traXstr);
                //        traY = float.Parse(traYstr);
                //        traZ = float.Parse(traZstr);

                //        //curObj.transform.Rotate(rotCor);

                //        curObjTrans = curObj.GetComponent<Transform>();

                //        //curHull = GameObject.Find(hullName);
                //        //curHullTrans = curHull.GetComponent<Transform>();


                //        pos = new Vector3(traX, traY, traZ);

                //        curObjTrans.position = pos;
                //        //curObjTrans.rotation = Quaternion.Euler(Vector3.zero);
                //        //curHullTrans.position = pos;
                //    }



            }
        }

        for (int l = 0; l < bodies.Count; l++)
        {
            XmlNode curPar = bodies.Item(l);

            bodyName = curPar.Attributes["name"].Value;

            curObj = GameObject.Find(bodyName);


            //    XmlNode offsetBody = curPar.SelectSingleNode("offsetfrom");
            //    parentName = offsetBody.InnerText;
            //    curObj.transform.SetParent(GameObject.Find(parentName).transform);

            XmlNodeList translations = curPar.SelectNodes("translation");

            {
                traX = 0;
                traY = 0;
                traZ = 0;
                for (int m = 0; m < translations.Count; m++)
                {
                    bodyTranslate = translations.Item(m).InnerText; //parent to current
                    //print("Ping");                                                //        //print(bodyTranslate);
                    space1Count = bodyTranslate.IndexOf(" ");
                    traXstr = bodyTranslate.Substring(0, space1Count);
                    space2Count = bodyTranslate.Substring(space1Count + 1, bodyTranslate.Length - (space1Count + 1)).IndexOf(" ");
                    traYstr = bodyTranslate.Substring(space1Count + 1, space2Count);
                    space2Count = space1Count + space2Count;
                    traZstr = bodyTranslate.Substring(space2Count + 1, bodyTranslate.Length - (space2Count + 1));

                    traX = float.Parse(traXstr) + traX;
                    traY = float.Parse(traYstr) + traY;
                    traZ = float.Parse(traZstr) + traZ;

                    //        //curObj.transform.Rotate(rotCor);

                    curObjTrans = curObj.GetComponent<Transform>();
                    pos = new Vector3(traX, traY, traZ);
                }
                //print(bodyName);
                //print(pos);
                //print(traX);
                //print(traY);
                //print(traZ);
                curObjTrans.localPosition = new Vector3(traX, traY, traZ);
                //curObjTrans.rotation = Quaternion.Euler(Vector3.zero);
                //        //curHullTrans.position = pos;
            }
        }



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
            }

            curJoint.connectedBody = parent.GetComponent<Rigidbody>();
            curJoint.axis = new Vector3(axX, axY, axZ); /////set AXIS
            //print(curJoint);



            //dont forget to set axis and fix limits

        }




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





    }
    void Update()
    {

        if (Input.GetKey("escape"))
            Application.Quit();
    }
}
//Use configurable joint limits to store limits in order to pass them to the joints script