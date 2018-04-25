using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    public float power;
    Rigidbody2D rigid2D;
	// Use this for initialization
	void Start () {
        rigid2D = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
        Vector2 dir = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.UpArrow))      dir += new Vector2(0, 1);
        if (Input.GetKeyDown(KeyCode.DownArrow))    dir += new Vector2(0,-1);
        if (Input.GetKeyDown(KeyCode.LeftArrow))    dir += new Vector2(-1,0);
        if (Input.GetKeyDown(KeyCode.RightArrow))   dir += new Vector2( 1,0);
        dir = Vector2.ClampMagnitude(dir, 1);
        if(dir != Vector2.zero)
            rigid2D.AddForce(dir * power);
        print(rigid2D.velocity.magnitude);
    }
}
