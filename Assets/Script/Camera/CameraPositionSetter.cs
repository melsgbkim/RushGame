using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionSetter : MonoBehaviour {
    public static CameraPositionSetter Get = null;

    Hashtable TargetList = new Hashtable();
    //List<GameObject> TargetList = new List<GameObject>();
    // Use this for initialization
    void Start () {
        if (Get == null)
            Get = this;

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 ave = Vector3.zero;
        foreach (Vector3ForCameraTarget t in TargetList.Values)
            ave += t.vec;
        ave /= TargetList.Count;
        transform.position = new Vector3(ave.x, ave.y, transform.position.z);


    }

    public Vector3ForCameraTarget AddTarget(string key)
    {
        Vector3ForCameraTarget vec = new Vector3ForCameraTarget();
        TargetList.Add(key, vec);
        return vec;
    }


}

public class Vector3ForCameraTarget
{
    public Vector3 vec = Vector3.zero;
}
