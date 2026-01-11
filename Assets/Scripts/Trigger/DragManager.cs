using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Zone
{
    public BlockType zoneType;
    public RectTransform rect;
}

public class DragManager : MonoBehaviour
{
    [SerializeField] private GameObject Lightbulb;
    [SerializeField] private GameObject Task;
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private GameObject TriggerZone;
    [SerializeField] private RectTransform _defaultLayer;
    [SerializeField] private RectTransform _dragLayer;
    [SerializeField] private List<Zone> zones = new List<Zone>();

    // 🔥 NOWE — podmiana obiektów
    [Header("Podmiana obiektów po ukończeniu zadania")]
    [SerializeField] private GameObject objectToHide;
    [SerializeField] private GameObject objectToShow;

    // 🔥 NOWE — dla GroupManager
    public bool IsCompleted { get; private set; } = false;

    private Rect _boundingBox;
    private DragObject _currentDraggedObject;

    private Vector3[] _cornersA = new Vector3[4];
    private Vector3[] _cornersB = new Vector3[4];

    public DragObject CurrentDraggedObject => _currentDraggedObject;

    private void Awake()
    {
        SetBoundingBoxRect(_dragLayer);
    }

    public void RegisterDraggedObject(DragObject drag)
    {
        _currentDraggedObject = drag;
        drag.transform.SetParent(_dragLayer);
    }

    public void UnregisterDraggedObject(DragObject drag)
    {
        drag.transform.SetParent(_defaultLayer);
        _currentDraggedObject = null;
    }

    public bool IsWithinBounds(Vector2 position)
    {
        return _boundingBox.Contains(position);
    }

    private void SetBoundingBoxRect(RectTransform rectTransform)
    {
        var corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        var position = corners[0];

        Vector2 size = new Vector2(
            rectTransform.lossyScale.x * rectTransform.rect.size.x,
            rectTransform.lossyScale.y * rectTransform.rect.size.y);

        _boundingBox = new Rect(position, size);
    }

    public void DetectIfInZone(DragObject drag)
    {
        RectTransform dragRect = drag.GetComponent<RectTransform>();
        var LightbulbObject = Lightbulb.GetComponent<Renderer>();
        var TaskObject = Task.GetComponent<Renderer>();

        foreach (var zone in zones)
        {
            if (IsRectOverlapping(dragRect, zone.rect))
            {
                if (drag.blockType == zone.zoneType)
                {
                    drag.transform.position = zone.rect.transform.position;
                    drag.locked = true;

                    if (AreAllZonesCompleted())
                    {
                        // 🔥 PODMIANA OBIEKTÓW (NOWE)
                        ReplaceObjects();

                        // 🔥 ORYGINALNE DZIAŁANIE
                        TriggerZone.SetActive(false);
                        uiPanel.SetActive(false);
                        LightbulbObject.material.SetColor("_BaseColor", Color.yellow);
                        TaskObject.material.SetColor("_BaseColor", Color.green);

                        // 🔥 dla GroupManager
                        IsCompleted = true;
                    }
                }
            }
        }
    }

    private bool AreAllZonesCompleted()
    {
        foreach (var zone in zones)
        {
            bool found = false;

            foreach (var drag in FindObjectsOfType<DragObject>())
            {
                if (drag.blockType == zone.zoneType && drag.locked)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
                return false;
        }

        return true;
    }

    // 🔥 NOWA FUNKCJA — podmiana obiektów
    private void ReplaceObjects()
    {
        if (objectToHide != null)
            objectToHide.SetActive(false);

        if (objectToShow != null)
        {
            objectToShow.transform.position = objectToHide.transform.position;
            objectToShow.transform.rotation = objectToHide.transform.rotation;
            objectToShow.transform.localScale = objectToHide.transform.localScale;

            objectToShow.SetActive(true);
        }
    }

    private bool IsRectOverlapping(RectTransform a, RectTransform b)
    {
        a.GetWorldCorners(_cornersA);
        b.GetWorldCorners(_cornersB);

        Rect rectA = new Rect(_cornersA[0], _cornersA[2] - _cornersA[0]);
        Rect rectB = new Rect(_cornersB[0], _cornersB[2] - _cornersB[0]);

        return rectA.Overlaps(rectB);
    }
}
