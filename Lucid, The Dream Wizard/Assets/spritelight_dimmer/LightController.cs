using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
   
    public float intensity = 1.0f;
    public Light lt;
    void Start()
    {
        lt = GetComponent<Light>();
    }
    void Update()
    {
        //float phi = Time.time / duration * 2 * Mathf.PI;
       // float amplitude = Mathf.Cos(phi) * 0.5F + 0.5F;
        lt.intensity = intensity;
    }
}