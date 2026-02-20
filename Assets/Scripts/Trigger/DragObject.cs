using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class DragObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private DragManager _manager;
    public BlockType blockType;

    public bool locked = false;

    private Vector2 _centerPoint;
    private Vector2 _worldCenterPoint => transform.TransformPoint(_centerPoint);

    private Vector3 _startPosition;

    private void Awake()
    {
        _manager = GetComponentInParent<DragManager>();
        _centerPoint = (transform as RectTransform).rect.center;
        _startPosition = transform.position;
    }

    public void ResetToStart()
    {
        transform.position = _startPosition;
        locked = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (locked) return;
        _manager.RegisterDraggedObject(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (locked) return;

        if (_manager.IsWithinBounds(_worldCenterPoint + eventData.delta))
        {
            transform.Translate(eventData.delta);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _manager.UnregisterDraggedObject(this);
        _manager.CheckIfAllZonesFilled();
    }
}
