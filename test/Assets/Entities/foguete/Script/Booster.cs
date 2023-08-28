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

    private float time = 0.0f;

    public bool aciveThrusters = true;

    public float speed = 2.0f;
    public float fuel = 3.0f;

    private Rigidbody rigidBody;
    private BoxCollider boxCollider;
    private bool couple = true;

    [HideInInspector]
    public Vector3 propageteVelocity = Vector3.zero;
    public UnityEvent discart;
    public UnityEvent thrust;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();

        if (discart == null)
        {
            discart = new UnityEvent();
        }
        if (thrust == null) {
            thrust = new UnityEvent();
        }
        else if (aciveThrusters)
        {
            ActiveThruster();
        }
        discart.AddListener(ToDiscard);
    }
    public bool AlturaPico() { 
        if (rigidBody.velocity.y < 0)
        {
            return true;
        }
        return false;
    }
    void Update()
    {
        if (aciveThrusters)
        {
            MoveOnTheTrajectory();
            if (EndPropulsion() && couple) {
                discart.Invoke();
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

        Wind.instance.RestTime();
    }
    private bool EndPropulsion()
    {
        fuel -= Time.deltaTime;
        if (fuel <= 0.0f) {
            return true;
        }
        return false;
    }
    public void ActiveThruster() {
        aciveThrusters = true;
        PrepareteRocket();
        thrust.Invoke();
    }
  
    public void MoveOnTheTrajectory() {
        if (!rigidBody.useGravity)
        {
            time += Time.deltaTime;

            Vector3 wind = Wind.instance.WindForces();

            float higth = componentY * time + gravity * Mathf.Pow(time, 2.0f) / 2.0f;
            float distance = componentX * time;

            Vector3 v = new Vector3(distance, higth, 0.0f);

            Quaternion rotation = Quaternion.AngleAxis(rotationY, Vector3.up);

            rigidBody.velocity = rotation * (v + propageteVelocity + wind);

            transform.up = rigidBody.velocity.normalized;
        }
    }
    private void ToDiscard() {
        couple = false;
        aciveThrusters = false;
        rigidBody.useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("onFloor")) {
            if (aciveThrusters)
            {
                discart.Invoke();
            }
            ToDiscard();
        }
    }
}
