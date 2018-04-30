using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    // Use this for initialization
    private void FixedUpdate()
    {
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.UpArrow)) dir += new Vector2(0, 1);
        if (Input.GetKey(KeyCode.DownArrow)) dir += new Vector2(0, -1);
        if (Input.GetKey(KeyCode.LeftArrow)) dir += new Vector2(-1, 0);
        if (Input.GetKey(KeyCode.RightArrow)) dir += new Vector2(1, 0);
        GetComponent<PlayerMove>().MoveUpdate(dir);
    }
}
