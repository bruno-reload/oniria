using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ParachuterControler : MonoBehaviour
{
    private Rigidbody rigidbody;
    public GameObject parachuter;
    public Transform parashuterBag;
    public float resitence = 0.1f;
    public float verticalAlignmentSpeed;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Hook();

        if (parachuter.activeInHierarchy)
        {
            VerticalAlignment();
        }
        if (Mathf.Acos(Vector3.Dot(Vector3.down, rigidbody.velocity.normalized)) * Mathf.Rad2Deg <= 60.0 && !parachuter.activeInHierarchy)
        {
            parachuter.SetActive(true);

            parachuter.transform.up = Vector3.up;
            Invoke("ParachuterPreparate", 0.51f);
        }
    }
    private void Hook() {
        parachuter.GetComponent<Transform>().position = parashuterBag.position;
    }
    private void VerticalAlignment() {
        if (parachuter.activeInHierarchy)
        {
            Quaternion up = Quaternion.Euler(Vector3.up);

            rigidbody.angularVelocity = Vector3.zero;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, up, verticalAlignmentSpeed * Time.deltaTime);

        }
    }
    private void ParachuterPreparate()
    {
        rigidbody.drag = resitence;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("onFloor")) {
            rigidbody.drag = 0.1f;
            parachuter.SetActive(false);
            enabled = false;
        }
    }
}
