using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    private const  float gravity = -9.8f;

    private float rocketAngle = 0.0F;
    private float rotationY = 0.0F;
    private float planeAngle = 30.0f;

    private float componentX;
    private float componentY;
    private float componentZ;

    public float speed = 2.0f;

    private float tg;

    private float mSin;
    private float mCos;


    private float spaceX;
    private float spaceY;

    private float ModuleSpaceX;
    private float ModuleSpaceY;

    private float deltaTime;
    private float time = 0.0f;

    private Rigidbody rigidBody;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        rocketAngle = transform.rotation.eulerAngles.z + 90;
        rotationY = transform.rotation.eulerAngles.y;


        componentY = speed * Mathf.Sin(rocketAngle * Mathf.Deg2Rad);
        componentX = speed * Mathf.Cos(rocketAngle * Mathf.Deg2Rad);

    }

    void Update()
    {
        time += Time.deltaTime;


        float higth = componentY * time + gravity * Mathf.Pow(time, 2.0f) / 2.0f;
        float distance = componentX * time;

        Vector3 v = new Vector3(distance, higth, 0.0f);

        Quaternion rotation = Quaternion.AngleAxis(rotationY, Vector3.up);

        rigidBody.velocity = rotation * v;

        transform.up = rigidBody.velocity.normalized;
    }
}
