using UnityEngine;
using UnityEngine.EventSystems;

public class RotateKnob : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public float currentAngle;
    public float minAngle = -45f;
    public float maxAngle = 45f;

    private float startDragAngle;
    private float startKnobAngle;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startDragAngle = GetPointerAngle(eventData.position);
        startKnobAngle = currentAngle;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float pointerAngle = GetPointerAngle(eventData.position);
        float delta = Mathf.DeltaAngle(startDragAngle, pointerAngle);

        float target = startKnobAngle + delta;

        // clamp
        currentAngle = Mathf.Clamp(target, minAngle, maxAngle);

        // UI obraca się TYLKO po clampie
        transform.localEulerAngles = new Vector3(0, 0, currentAngle);
    }

    private float GetPointerAngle(Vector2 pointerPos)
    {
        Vector2 dir = pointerPos - (Vector2)transform.position;
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
}
