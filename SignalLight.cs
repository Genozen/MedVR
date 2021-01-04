using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalLight : MonoBehaviour {

    public float duration = 4.0F;
    public Light lt;
    void Start()
    {
        lt = GetComponent<Light>();
    }
    void Update()
    {
        float phi = Time.time / duration * 2 * Mathf.PI;
        float amplitude = Mathf.Cos(phi) * 5.5F + 0.5F;
        lt.intensity = amplitude;
    }
}
