using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ParachuterControler : MonoBehaviour
{
    private Rigidbody rigidbody;
    public GameObject parachuter;
    private Animator animation;
    public Transform parashuterBag;
    public float resitence = 0.1f;
    public float verticalAlignmentSpeed;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animation = parachuter.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        VerticalAlignment();
        if (Mathf.Acos(Vector3.Dot(Vector3.down, rigidbody.velocity.normalized)) * Mathf.Rad2Deg <= 60.0 && !parachuter.activeInHierarchy)
        {
            Invoke("ParachuterPreparate", 0.51f);
        }
    }
    private void UpdatePosition() {
        parachuter.GetComponent<Transform>().position = parashuterBag.position;
    }
    private void VerticalAlignment() {
        if (parachuter.activeInHierarchy)
        {
            Quaternion up = Quaternion.Euler(Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, up, verticalAlignmentSpeed * Time.deltaTime);
        }
    }
    private void ParachuterPreparate() {
        GetComponent<Booster>().aciveThrusters = false;
        rigidbody.drag = resitence;
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.useGravity = true;
        parachuter.SetActive(true);
        animation.Play("Open");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("onFloor")) {
            rigidbody.drag = 0.0f;
        }
    }
}
