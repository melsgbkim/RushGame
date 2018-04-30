using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    public float MovePower;
    public float MoveRepeatTime;

    float timeSum = 0f;
    Rigidbody2D rigid2D;
    Vector3ForCameraTarget vec3BodyPosition;
    Vector3ForCameraTarget vec3MoveTo;

    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();

        vec3BodyPosition = CameraPositionSetter.Get.AddTarget(gameObject.ToString() + "Body");
        vec3MoveTo = CameraPositionSetter.Get.AddTarget(gameObject.ToString() + "MoveTo");
    }
    public void MoveUpdate(Vector2 dir)
    {
        timeSum += Time.deltaTime;
        if (timeSum >= MoveRepeatTime)
        {
            int multiple = Mathf.RoundToInt(timeSum / MoveRepeatTime);
            timeSum -= multiple * MoveRepeatTime;

            dir = Vector2.ClampMagnitude(dir, 1);
            if (dir != Vector2.zero)
                rigid2D.AddForce(dir * MovePower);
        }

        if (Input.GetKeyDown(KeyCode.Space)) rigid2D.AddForce(dir * MovePower * 40);
        //for test
    }

    // Update is called once per frame
    void Update()
    {
        vec3BodyPosition.vec = transform.position;
        vec3MoveTo.vec = Util.V3toV2(transform.position) + rigid2D.velocity * Time.deltaTime;
    }
}
