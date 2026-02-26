using UnityEngine;
using UnityEngine.EventSystems;

public class RotateKnob : MonoBehaviour, IDragHandler
{
    public float currentAngle;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 dir = eventData.position - (Vector2)transform.position;
        currentAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }
}
