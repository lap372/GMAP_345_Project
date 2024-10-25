using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public AnimationCurve rotationXCurve; // Curve for X-axis rotation speed
    public AnimationCurve rotationYCurve; // Curve for Y-axis rotation speed
    public AnimationCurve rotationZCurve; // Curve for Z-axis rotation speed
    public float duration = 5f;            // Duration over which the curves will be evaluated

    private float timeElapsed = 0f;       // Time elapsed since the start

    void Update()
    {
        // Increment the time elapsed
        timeElapsed += Time.deltaTime;

        // Normalize time to the range of [0, 1] based on duration
        float normalizedTime = Mathf.Clamp01(timeElapsed / duration);

        // Get the rotation speeds from the curves
        float rotationSpeedX = rotationXCurve.Evaluate(normalizedTime);
        float rotationSpeedY = rotationYCurve.Evaluate(normalizedTime);
        float rotationSpeedZ = rotationZCurve.Evaluate(normalizedTime);

        // Rotate the GameObject based on the evaluated speeds
        transform.Rotate(rotationSpeedX * Time.deltaTime, rotationSpeedY * Time.deltaTime, rotationSpeedZ * Time.deltaTime);
    }
}
