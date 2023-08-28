using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{

    private float windXYForce;
    private float windZYForce;
    public float windVelocity;
    private float time;
    private Vector3 startWind = Vector3.zero;
    public static Wind instance;
    void Awake()
    {
        instance = this;
    }

    public Vector3 WindForces() {
        startWind = Vector3.Lerp(startWind, transform.forward * windVelocity, Mathf.Clamp(time,0,1));
        return startWind;
    }
    public void Update()
    {
        if( time >= 0)
            time += Time.deltaTime / (windVelocity * 10.0f);
    }
    public void RestTime() {
        time = 0;
    }

}
