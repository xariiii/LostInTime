using UnityEngine;

public class Knob3DRotator : MonoBehaviour
{
    public RotateKnob knobUI;
    public float rotationMultiplier = 1f;
    public bool invert = false; // ← ustaw to na true lub false zależnie od modelu

    void Update()
    {
        float uiAngle = knobUI.currentAngle * rotationMultiplier;

        if (invert)
            uiAngle = -uiAngle;

        Vector3 e = transform.localEulerAngles;
        e.x = uiAngle; // ← tylko X, Y/Z zostają
        transform.localEulerAngles = e;
    }
}
