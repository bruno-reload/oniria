using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CouplerTools
{
    [RequireComponent(typeof(Booster))]
    public class Coupler : MonoBehaviour
    {
        public Transform positionForCurrentModule;
        private bool isDocking = true;
        private Booster boosterControl;
        public Rigidbody baseModule;

        void Start() { 

            boosterControl = GetComponent<Booster>();

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
            transform.position = positionForCurrentModule.position - positionForCurrentModule.up * positionForCurrentModule.localPosition.y;
            transform.up = positionForCurrentModule.up;
        }
  
        public void Uncouple()
        {
            isDocking = false;
            TransferForces();
        }

        public void TransferForces()
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