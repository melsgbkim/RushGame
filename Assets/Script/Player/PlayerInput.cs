using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    public float MovePower;
    public float MoveRepeatTime;

    float timeSum = 0f;
    Rigidbody2D rigid2D;
    Vector3ForCameraTarget vec3BodyPosition;
    Vector3ForCameraTarget vec3MoveTo;
    // Use this for initialization
    void Start () {
        rigid2D = GetComponent<Rigidbody2D>();

        vec3BodyPosition = CameraPositionSetter.Get.AddTarget(gameObject.ToString() + "Body");
        vec3MoveTo = CameraPositionSetter.Get.AddTarget(gameObject.ToString() + "MoveTo");
    }
    private void FixedUpdate()
    {
        Vector2 dir = Vector2.zero;
        timeSum += Time.deltaTime;
        if (timeSum >= MoveRepeatTime)
        {
            int multiple = Mathf.RoundToInt(timeSum / MoveRepeatTime);
            timeSum -= multiple * MoveRepeatTime;
            if (Input.GetKey(KeyCode.UpArrow)) dir += new Vector2(0, 1);
            if (Input.GetKey(KeyCode.DownArrow)) dir += new Vector2(0, -1);
            if (Input.GetKey(KeyCode.LeftArrow)) dir += new Vector2(-1, 0);
            if (Input.GetKey(KeyCode.RightArrow)) dir += new Vector2(1, 0);
            
            dir = Vector2.ClampMagnitude(dir, 1);
            if (dir != Vector2.zero)
                rigid2D.AddForce(dir * MovePower);

            
        }

        if (Input.GetKeyDown(KeyCode.Space)) rigid2D.AddForce(new Vector2(1, 0) * MovePower * 30);

    }

    // Update is called once per frame
    void Update () {
        vec3BodyPosition.vec = transform.position;
        vec3MoveTo.vec = Util.V3toV2(transform.position) + rigid2D.velocity * Time.deltaTime;
    }
}
