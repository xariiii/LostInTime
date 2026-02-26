using UnityEngine;

public class LaserRotator : MonoBehaviour
{
    public RotateKnob knob;
    public float rotationMultiplier = 1f;

    void Update()
    {
        float zRotation = knob.currentAngle * rotationMultiplier;
        transform.rotation = Quaternion.Euler(0, 0, zRotation);
    }
}
