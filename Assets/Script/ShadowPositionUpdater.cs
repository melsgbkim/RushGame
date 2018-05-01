using UnityEngine;
using System.Collections;

public class ShadowPositionUpdater : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 v = transform.localPosition;
        transform.localPosition = new Vector3(v.x, v.y, -transform.parent.localPosition.z);
    }
}
