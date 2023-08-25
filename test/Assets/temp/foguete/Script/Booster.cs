using CouplerTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Booster : MonoBehaviour
{
    private const  float gravity = -9.8f;

    private float rocketAngle = 0.0F;
    private float rotationY = 0.0F;

    private float componentX;
    private float componentY;

    public float time = 0.0f;

    public bool aciveThrusters = true;

    public float speed = 2.0f;
    public float fuel = 3.0f;

    private Rigidbody rigidBody;
    private BoxCollider boxCollider;
    public UnityEvent endLife;
    private bool couple = true;

    public Vector3 propageteVelocity = Vector3.zero;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();

        PrepareteRocket();

        if (endLife == null)
        {
            endLife = new UnityEvent();
        }
        endLife.AddListener(ToDiscard);

    }

    void Update()
    {
        if (aciveThrusters)
        {
            MoveOnTheTrajectory();
            if (EndPropulsion() && couple) {
                endLife.Invoke();
            }
        }
    }    
    public void PrepareteRocket() {
        boxCollider.isTrigger = false;
        rocketAngle = transform.rotation.eulerAngles.z + 90;
        rotationY = transform.rotation.eulerAngles.y;
        time = 0.0f;


        componentY = speed * Mathf.Sin(rocketAngle * Mathf.Deg2Rad);
        componentX = speed * Mathf.Cos(rocketAngle * Mathf.Deg2Rad);
    }
    private bool EndPropulsion()
    {
        fuel -= Time.deltaTime * speed;
        if (fuel <= 0.0f) {
            return true;
        }
        return false;
    }
    public void ActiveThruster() {
        aciveThrusters = true;
    }
  
    public void MoveOnTheTrajectory() {

        time += Time.deltaTime;

        float higth = componentY * time + gravity * Mathf.Pow(time, 2.0f) / 2.0f;
        float distance = componentX * time;

        Vector3 v = new Vector3(distance, higth, 0.0f);

        Quaternion rotation = Quaternion.AngleAxis(rotationY, Vector3.up);

        rigidBody.velocity = rotation * (v + propageteVelocity);

        transform.up = rigidBody.velocity.normalized;
    }
    private void ToDiscard() {
        couple = false;
        rigidBody.useGravity = true;
        aciveThrusters = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("onFloor")) {
            ToDiscard();
        }
    }
}
