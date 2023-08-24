using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Booster : MonoBehaviour
{
    private const  float gravity = -9.8f;

    private float rocketAngle = 0.0F;
    private float rotationY = 0.0F;

    private float componentX;
    private float componentY;

    public float speed = 2.0f;
    private float time = 0.0f;

    public bool aciveThrusters = true;

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
        if (aciveThrusters)
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
}
