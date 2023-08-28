using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CouplerTools
{
    [RequireComponent(typeof(Booster))]
    public class Coupler : MonoBehaviour
    {
        private bool isDocking = true;
        private Booster boosterControl;
        public Rigidbody baseModule;
        private BoxCollider boxCollider;
        private float distance;
        private Transform buffer;

        void Start() { 

            boosterControl = GetComponent<Booster>();
            boxCollider = GetComponent<BoxCollider>();
            distance = Vector3.Distance(transform.position,  baseModule.transform.position);
        }
        void Update()
        {
            if (isDocking)
            {
                TransferControl();
            }
        }

        private void TransferControl()
        {
            transform.position = baseModule.transform.position + baseModule.transform.up * distance;
            transform.up = baseModule.transform.up;
        }
  
        public void Uncouple()
        {
            isDocking = false;
            TransferForces();
        }

        private void TransferForces()
        {
            boosterControl.propageteVelocity = baseModule.velocity;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("onFloor"))
            {
                isDocking = false;
            }
        }
    }
}