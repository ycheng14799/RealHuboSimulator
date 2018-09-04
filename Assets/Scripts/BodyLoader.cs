using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyLoader : MonoBehaviour {

    public const string path = "hubo2.kinbody";

    void Start()
    {
        BodyContainer bc = BodyContainer.Load(path);

        foreach(Body body in bc.bodies)
        {
            print(body.name);
        }
    }

}
