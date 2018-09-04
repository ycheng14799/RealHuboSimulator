using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionLock : MonoBehaviour {

    private Transform local;
    private Vector3 localPos;
    private ConfigurableJoint configJoint;
    private Rigidbody joint;

	void Start () {
        joint = GetComponent<Rigidbody>();
        local = joint.transform;
        configJoint = GetComponent<ConfigurableJoint>();
        localPos = local.localPosition;

        //if (configJoint != null)
        //{

        //    configJoint.angularXMotion = ConfigurableJointMotion.Locked;
        //    configJoint.angularYMotion = ConfigurableJointMotion.Locked;
        //    configJoint.angularZMotion = ConfigurableJointMotion.Locked;
        //    configJoint.xMotion = ConfigurableJointMotion.Locked;
        //    configJoint.yMotion = ConfigurableJointMotion.Locked;
        //    configJoint.zMotion = ConfigurableJointMotion.Locked;

        //    if (configJoint.axis.x != 0)
        //    {
        //        joint.constraints = RigidbodyConstraints.None;
        //        joint.constraints = RigidbodyConstraints.FreezeRotationX;
        //    }
        //    else if (configJoint.axis.y != 0)
        //    {
        //        joint.constraints = RigidbodyConstraints.None;
        //        joint.constraints = RigidbodyConstraints.FreezeRotationY;
        //    }
        //    else if (configJoint.axis.z != 0)
        //    {
        //        joint.constraints = RigidbodyConstraints.None;
        //        joint.constraints = RigidbodyConstraints.FreezeRotationZ;
        //    }
        //}

        //local.localRotation = Quaternion.Euler(Vector3.zero);
    }
	    
	void Update () {

        local.localPosition.Set(localPos.x, localPos.y, localPos.z);

    }
}
