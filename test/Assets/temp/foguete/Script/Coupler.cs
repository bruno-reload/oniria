using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coupler : MonoBehaviour
{

    public Transform target;
    private Vector3 lastPosition;
    private bool isCouple = true;
    void Start()
    {
        lastPosition = transform.position - target.position;
    }
    void Update()
    {
        if (isCouple)
        {
            transform.position = target.position - target.up * target.localPosition.y;
            transform.up = target.up;
        }
    }
}
