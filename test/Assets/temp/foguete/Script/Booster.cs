using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    private const  float gravity = -9.8f;
    private float angle;

    private float v_y;
    private float v_x;

    public float speed;

    private float time;
    void Start()
    {
        v_y = speed * Mathf.Sin(angle);
        v_x = speed * Mathf.Cos(angle);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        
    }
}
