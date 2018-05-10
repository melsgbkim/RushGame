using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionSetter : MonoBehaviour {
    public static CameraPositionSetter Get = null;

    Hashtable TargetList = new Hashtable();
    //List<GameObject> TargetList = new List<GameObject>();
    // Use this for initialization
    List<float> SizeValueList = new List<float>();

    Vector3 ave;
    public Camera camera;
    public CameraPositionSetter()
    {
        if (Get == null)
            Get = this;
    }
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
        PositionUpdate();
        SizeUpdate();
    }

    void SizeUpdate()
    {
        float MaxDifferenceFromAve = 0f;
        foreach (Vector3ForCameraTarget t in TargetList.Values)
        {
            if (MaxDifferenceFromAve < (ave - t.vec).magnitude)
                MaxDifferenceFromAve = (ave - t.vec).magnitude;
        }
        SizeValueList.Add(2f + MaxDifferenceFromAve * 5f);
        float average = 0f;
        foreach (float v in SizeValueList)
            average += v / SizeValueList.Count;
        if (SizeValueList.Count > 20)
            SizeValueList.RemoveAt(0);
        camera.orthographicSize = average;
    }

    void PositionUpdate()
    {
        ave = Vector3.zero;
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
